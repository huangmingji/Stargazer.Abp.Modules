namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{
    public class ClickRequest
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
        /// 事件类型，CLICK、VIEW
        /// </summary>
        public string Event { get; set; } = "";

        /// <summary>
        /// 事件KEY值，Event=CLICK与自定义菜单接口中KEY值对应，Event=VIEW设置的跳转URL
        /// </summary>
        public string EventKey { get; set; } = "";
    }
}
