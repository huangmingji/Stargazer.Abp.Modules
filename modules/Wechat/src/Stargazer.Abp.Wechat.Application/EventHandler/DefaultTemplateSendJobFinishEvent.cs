using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Wechat.Application.EventHandler
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultTemplateSendJobFinishEvent : ApplicationService, ITemplateSendJobFinishEvent
    {
        // private ITemplateMessageStore templateMessageStore;
        // public TemplateSendJobFinishEvent(ITemplateMessageStore templateMessageStore)
        // {
        //     this.templateMessageStore = templateMessageStore;
        // }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wechat"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<NormalMessage> HandleAsync(WechatDataDto wechat, TemplateSendJobFinishRequest request)
        {
            // TemplateMessageStatus status = TemplateMessageStatus.Send;
            // string message = "";

            // switch (request.Status)
            // {
            //     case "success":
            //         status = TemplateMessageStatus.Success;
            //         message = "���ͳɹ�";
            //         break;
            //     case "failed: system failed":
            //         status = TemplateMessageStatus.Fail;
            //         message = "����ʧ��";
            //         break;
            //     case "failed:user block":
            //         status = TemplateMessageStatus.Fail;
            //         message = "�û��ܾ�����";
            //         break;
            // }

            // var result = await templateMessageStore.UpdateTemplateMessageStatus(wechat.AppId, request.MsgID, status, message);
            return null;
        }
    }
}