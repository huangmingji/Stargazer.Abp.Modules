namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{
    public class ScanRequest
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
        /// 事件类型，SCAN
        /// </summary>
        public string Event { get; set; } = "";

        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; } = "";

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; } = "";
    }
}
