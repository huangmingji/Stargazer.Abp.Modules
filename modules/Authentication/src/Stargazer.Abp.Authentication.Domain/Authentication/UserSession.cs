using Volo.Abp.Domain.Entities.Auditing;

namespace Stargazer.Abp.Authentication.Domain.Authentication;

public class UserSession : AuditedAggregateRoot<Guid>
{
    public UserSession(Guid id,
        Guid userId,
        Guid? deviceId,
        string accessToken,
        string refreshToken,
        long refreshTokenExpires,
        string ipAddress,
        string geoLocation,
        bool isExclusive) : base(id)
    {
        UserId = userId;
        DeviceId = deviceId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        RefreshTokenExpires = refreshTokenExpires;
        IpAddress = ipAddress;
        GeoLocation = geoLocation;
        IsExclusive = isExclusive;
    }
    
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
    public long RefreshTokenExpires { get; protected set; }

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
    public bool IsActive { get; protected set; } = true;

    /// <summary>
    /// 多端互斥控制,是否独占会话
    /// </summary>
    public bool IsExclusive { get; protected set; }

    public UserDevice? Device { get; protected set; }
    
    public void SetAccessToken(string accessToken)
    {
        AccessToken = accessToken;
    }

    public void SetRefreshToken(string refreshToken, long refreshTokenExpires)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpires = refreshTokenExpires;
    }
    
    public void SetInactive()
    {
        IsActive = false;
    }
    
    public void SetExclusive(bool isExclusive)
    {
        IsExclusive = isExclusive;
    }

    public void SetIpAddress(string ipAddress)
    {
        IpAddress = ipAddress;
    }
}