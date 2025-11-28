namespace Stargazer.Abp.Wechat.Domain.Shared.Enumes
{
    public enum EncodeType
    {
        /// <summary>
        /// 明文模式
        /// </summary>
        None = 0,

        /// <summary>
        /// 安全模式（aes加密）
        /// </summary>
        Aes = 1,

        /// <summary>
        /// 兼容模式
        /// </summary>
        mixture = 2
    }
}