namespace Stargazer.Abp.Wechat.Domain.Shared.Enumes;

/// <summary>
/// 模板消息状态
/// </summary>
public enum TemplateMessageStatus
{
    /// <summary>
    /// 待发送
    /// </summary>
    Send = 0,

    /// <summary>
    /// 发送成功
    /// </summary>
    Success = 1,

    /// <summary>
    /// 发送失败
    /// </summary>
    Fail = -1,

    /// <summary>
    /// 已提交
    /// </summary>
    Commit = 2
}