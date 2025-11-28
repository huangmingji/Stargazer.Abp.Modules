using Volo.Abp.Application.Dtos;

namespace Stargazer.Abp.Authentication.Application.Contracts;

public class UserSessionDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// 关联用户
    /// </summary>
    public Guid UserId { get; protected set; }

    /// <summary>
    /// 关联设备
    /// </summary>
    public Guid? DeviceId { get; protected set; }
    
    /// <summary>
    /// 授权令牌
    /// </summary>
    public string AccessToken { get; protected set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; protected set; }

    /// <summary>
    /// 刷新令牌过期时间
    /// </summary>
    public DateTime RefreshTokenExpires { get; protected set; }

    /// <summary>
    /// 登录地址
    /// </summary>
    public string IpAddress { get; protected set; }

    /// <summary>
    /// 地理位置
    /// </summary>
    public string GeoLocation { get; protected set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool IsActive { get; protected set; }

    /// <summary>
    /// 多端互斥控制,是否独占会话
    /// </summary>
    public bool IsExclusive { get; protected set; }

    public UserDeviceDto? Device { get; protected set; }
}