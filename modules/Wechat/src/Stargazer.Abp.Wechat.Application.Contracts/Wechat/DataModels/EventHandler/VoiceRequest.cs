namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{
    public class VoiceRequest
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
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; } = "";

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; } = "";

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgID { get; set; } = "";
    }
}
