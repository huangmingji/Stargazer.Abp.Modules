using Stargazer.Abp.Authentication.Domain.Shared.Authentication;
using Volo.Abp.Application.Dtos;

namespace Stargazer.Abp.Authentication.Application.Contracts;

public class UserDeviceDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// 用户主键
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public DeviceType DeviceType { get; set; }

    /// <summary>
    /// 设备操作系统
    /// </summary>
    public string DeviceOS { get; set; }

    /// <summary>
    /// 设备操作系统版本
    /// </summary>
    public string DeviceOSVersion { get; set; }

    /// <summary>
    /// 设备浏览器
    /// </summary>
    public string DeviceBrowser { get; set; }

    /// <summary>
    /// 设备浏览器版本
    /// </summary>
    public string DeviceBrowserVersion { get; set; }

    /// <summary>
    /// 设备IP
    /// </summary>
    public string DeviceIp { get; set; }

    /// <summary>
    /// 设备地理位置
    /// </summary>
    public string DeviceLocation { get; set; }

    /// <summary>
    /// 设备用户代理
    /// </summary>
    public string DeviceUserAgent { get; set; }
    
    /// <summary>
    /// 推送令牌
    /// </summary>
    public string PushToken { get; protected set; }
    
    /// <summary>
    /// 最后活跃时间
    /// </summary>
    public DateTime LastActivityTime { get; protected set; }
    
    /// <summary>
    /// 是否受信设备
    /// </summary>
    public bool IsTrusted { get; protected set; }
}