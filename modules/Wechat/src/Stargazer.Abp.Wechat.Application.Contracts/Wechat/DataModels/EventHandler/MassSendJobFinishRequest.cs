namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler
{
    public class MassSendJobFinishRequest
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; } = "";

        /// <summary>
        /// 公众号群发助手的微信号，为mphelper
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
        /// 事件信息，此处为MASSSENDJOBFINISH
        /// </summary>
        public string Event { get; set; } = "";

        /// <summary>
        /// 群发的消息ID
        /// </summary>
        public string MsgID { get; set; } = "";

        /// <summary>
        /// 群发的结果，为“send success”或“send fail”或“err(num)”。但send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。err(num)是审核失败的具体原因，可能的情况如下：
        /// err(10001), 涉嫌广告
        /// err(20001), 涉嫌政治
        /// err(20004), 涉嫌社会
        /// err(20002), 涉嫌色情
        /// err(20006), 涉嫌违法犯罪
        /// err(20008), 涉嫌欺诈
        /// err(20013), 涉嫌版权
        /// err(22000), 涉嫌互推(互相宣传)
        /// err(21000), 涉嫌其他
        /// err(30001), 原创校验出现系统错误且用户选择了被判为转载就不群发
        /// err(30002), 原创校验被判定为不能群发
        /// err(30003), 原创校验被判定为转载文且用户选择了被判为转载就不群发
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// tag_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public string TotalCount { get; set; } = "";

        /// <summary>
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public string FilterCount { get; set; } = "";

        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public string SentCount { get; set; } = "";

        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public string ErrorCount { get; set; } = "";

        /// <summary>
        /// 各个单图文校验结果
        /// </summary>
        public CopyrightCheckResult CopyrightCheckResult { get; set; } = new CopyrightCheckResult();
    }

    public class CopyrightCheckResult
    {
        public string Count { get; set; } = "";

        /// <summary>
        /// 整体校验结果 1-未被判为转载，可以群发，2-被判为转载，可以群发，3-被判为转载，不能群发
        /// </summary>
        public string CheckState { get; set; } = "";

        public List<item> ResultList { get; set; } = new List<item>();
    }

    public class item
    {
        /// <summary>
        /// 群发文章的序号，从1开始
        /// </summary>
        public string ArticleIdx { get; set; } = "";

        /// <summary>
        /// 用户声明文章的状态
        /// </summary>
        public string UserDeclareState { get; set; } = "";

        /// <summary>
        /// 系统校验的状态
        /// </summary>
        public string AuditState { get; set; } = "";

        /// <summary>
        /// 相似原创文的url
        /// </summary>
        public string OriginalArticleUrl { get; set; } = "";

        /// <summary>
        /// 相似原创文的类型
        /// </summary>
        public string OriginalArticleType { get; set; } = "";

        /// <summary>
        /// 是否能转载
        /// </summary>
        public string CanReprint { get; set; } = "";

        /// <summary>
        /// 是否需要替换成原创文内容
        /// </summary>
        public string NeedReplaceContent { get; set; } = "";

        /// <summary>
        /// 是否需要注明转载来源
        /// </summary>
        public string NeedShowReprintSource { get; set; } = "";
    }
}
