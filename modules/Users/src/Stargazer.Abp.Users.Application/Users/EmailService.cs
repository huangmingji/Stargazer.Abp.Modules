using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stargazer.Abp.Users.Domain.Users;
using Stargazer.Common.Extend;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating;

namespace Stargazer.Abp.Users.Application.Users;

public class EmailService(
    ITemplateRenderer templateRenderer,
    IConfiguration configuration,
    IEmailSender emailSender,
    IDistributedCache<string> cache,
    ILogger<EmailService> logger)
    : ITransientDependency
{
    public async Task EmailChanged(EmailChangedEvent eventData)
    {
        try
        {
            logger.LogInformation($"###EmailChangedEventHandler###-------{eventData.Email} verify start");
            bool verifyEmail = configuration.GetSection("App:VerifyEmail").Value?.ToBool() ?? false;
            if (!verifyEmail)
            {
                logger.LogInformation($"###EmailChangedEventHandler###-------{eventData.Email} not need verify");
                return;
            }

            string key = $"VerifyEmail:{eventData.Email}";
            string? token = await cache.GetAsync(key);
            if (!string.IsNullOrWhiteSpace(token))
            {
                logger.LogInformation($"###EmailChangedEventHandler###-------{eventData.Email} token have sent");
                return;
            }

            string host = configuration.GetSection("App:Host").Value ?? "";
            token = Ext.CreateNonceStr(128);
            await cache.SetAsync($"VerifyEmail:{eventData.Email}", token, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20)
            });

            StringBuilder message = new();
            message.Append("<div>");
            message.Append($"<p>你好，{eventData.User.NickName}！</p>");
            message.Append($"<p>有人（希望是您）要求在 <a href='{host}'>{host}</a> 上确认您的电子邮件地址</p>");
            message.Append("<p>如果您没有执行此请求，您可以安全地忽略此电子邮件</p>");
            message.Append($"<p>请复制下面的验证码以在<a href='{host}'>{host}</a>上验证激活您的电子邮件地址（<a href='mailto:{eventData.Email}'>{eventData.Email}</a>）</p>");
            message.Append($"<p>{token}</p>");
            message.Append("<p>以上验证码有效时间为20分钟，请在有效期内完成验证</p>");
            message.Append("</div>");

            var body = await templateRenderer.RenderAsync(StandardEmailTemplates.Message,
                new
                {
                    message = message.ToString()
                }
            );
            string subject = "确认说明";
            await emailSender.SendAsync(
                to: eventData.Email, // target email address
                subject: subject, // subject
                body: body // email body
            );
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
        finally
        {
            logger.LogInformation($"###EmailChangedEventHandler###-------{eventData.Email} verify end");
        }
    }

    public async Task FindPassword(FindPasswordEvent eventData)
    {
        try
        {
            logger.LogInformation($"###FindPasswordEventHandle###-------{eventData.Email} find password start");
            var token = Ext.CreateNonceStr(64);
            await cache.SetAsync($"FindPasswordToken:{eventData.Email}", token, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20)
            });
            string host = configuration.GetSection("App:Host").Value ?? "";
            StringBuilder message = new();
            message.Append("<div>");
            message.Append($"<p>{eventData.User.NickName}，您好!</p>");
            message.Append($"<p>有人（希望是您）要求在 <a href='{host}'>{host}</a> 上重置您的账号的密码</p>");
            message.Append("<p>如果您没有执行此请求，您可以安全地忽略此电子邮件</p>");
            message.Append($"<p>请复制下面的验证码以在<a href='{host}'>{host}</a>上验证重置您的账号的密码</p>");
            message.Append($"<p>{token}</p>");
            message.Append("<p>以上验证码有效时间为20分钟，请在有效期内完成验证</p>");
            message.Append("</div>");
            var body = await templateRenderer.RenderAsync(
                StandardEmailTemplates.Message,
                new
                {
                    message = message.ToString()
                }
            );
            string subject = "重置密码说明";
            await emailSender.SendAsync(
                to: eventData.Email, // target email address
                subject: subject, // subject
                body: body // email body
            );
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
        finally
        {
            logger.LogInformation($"###FindPasswordEventHandle###-------{eventData.Email} find password end");
        }
    }
}