namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{
    public class TextRequest
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
        /// 消息类型，text
        /// </summary>
        public string MsgType { get; set; } = "";

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; } = "";

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; } = "";
    }
}
