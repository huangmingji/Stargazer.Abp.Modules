namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{
    public class LocationRequest
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
        /// 消息类型，link
        /// </summary>
        public string MsgType { get; set; } = "";

        /// <summary>
        /// 消息类型为location时，返回地理位置维度
        /// </summary>
        public string Location_X { get; set; } = "";

        /// <summary>
        /// 消息类型为location时，返回地理位置经度
        /// </summary>
        public string Location_Y { get; set; } = "";

        /// <summary>
        /// 消息类型为location时，返回地图缩放大小
        /// </summary>
        public string Scale { get; set; } = "";

        /// <summary>
        /// 消息类型为location时，返回地理位置信息
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; } = "";
    }
}
