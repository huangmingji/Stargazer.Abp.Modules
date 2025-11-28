using System;
using System.Xml.Serialization;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels
{
    [XmlRoot("xml")]
    public class RequestData
	{
        // <summary>
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
        /// 信息类型，地理位置：location，文本消息：text，消息类型：image，语音类型：Voice，视频类型：Video，链接类型：Link，事件类型：Event， 小视频：ShortVideo
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

        /// <summary>
        /// 图片链接（由系统生成）
        /// </summary>
        public string PicUrl { get; set; } = "";

        /// <summary>
        /// 信息类型为image时，返回图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// 信息类型为voice时，返回语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// 消息类型为video时，返回视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// 消息类型为shortvideo时，返回视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; } = "";

        /// <summary>
        /// 信息类型为voice时，返回语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; } = "";

        /// <summary>
        /// 开通语音识别后，返回语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; } = "";

        /// <summary>
        /// 消息类型为video时，返回视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// 消息类型为shortvideo时，返回视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; } = "";

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
        /// 消息类型为link时，返回消息标题
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 消息类型为link时，返回消息描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 消息类型为link时，返回消息链接
        /// </summary>
        public string Url { get; set; } = "";

        /// <summary>
        /// 事件类型。
        /// 用户关注：subscribe；
        /// 取消关注：unsubscribe；
        /// 扫描带参数二维码事件：用户未关注时为subscribe，用户已关注时为SCAN；
        /// 模板消息发送后返回：TEMPLATESENDJOBFINISH；
        /// 上报地理位置事件：LOCATION
        /// 点击菜单拉取消息：CLICK
        /// 点击菜单跳转链接：VIEW
        /// 仅在MsgType=event时返回。
        /// </summary>
        public string Event { get; set; } = "";

        /// <summary>
        /// 事件KEY值。
        /// 点击菜单拉取消息时，与自定义菜单接口中KEY值对应；
        /// 点击菜单跳转链接时，设置的跳转URL
        /// 扫描带参数二维码事件，用户未关注时，qrscene_为前缀，后面为二维码的参数值；
        /// 扫描带参数二维码事件，用户已关注时，是一个32位无符号整数，即创建二维码时的二维码scene_id；
        /// </summary>
        public string EventKey { get; set; } = "";

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片。扫描带参数二维码事件时返回。
        /// </summary>
        public string Ticket { get; set; } = "";

        /// <summary>
        /// 地理位置纬度。仅在上报地理位置事件。
        /// </summary>
        public string Latitude { get; set; } = "";

        /// <summary>
        /// 地理位置经度。仅在上报地理位置事件。
        /// </summary>
        public string Longitude { get; set; } = "";

        /// <summary>
        /// 地理位置精度。仅在上报地理位置事件。
        /// </summary>
        public string Precision { get; set; } = "";
    }
}

