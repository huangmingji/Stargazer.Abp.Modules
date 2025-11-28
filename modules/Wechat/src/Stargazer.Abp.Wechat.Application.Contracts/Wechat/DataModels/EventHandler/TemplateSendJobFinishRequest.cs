namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{

    public class TemplateSendJobFinishRequest
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; } = "";

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; } = "";

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; } = "";

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; } = "";

        /// <summary>
        /// 事件为模板消息发送结束 TEMPLATESENDJOBFINISH
        /// </summary>
        public string Event { get; set; } = "";

        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgID { get; set; } = "";

        /// <summary>
        /// success发送状态为成功；
        /// failed: system failed发送状态为发送失败（非用户拒绝）
        /// failed:user block 发送状态为用户拒绝接收
        /// </summary>
        public string Status { get; set; } = "";
    }
}
