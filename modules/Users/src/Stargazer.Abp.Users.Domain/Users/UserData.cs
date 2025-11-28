
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Stargazer.Abp.Users.Domain.Shared.Users;
using Stargazer.Common;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Stargazer.Abp.Users.Domain.Users;

public class UserData : AuditedAggregateRoot<Guid>, ISoftDelete
{
    public UserData(Guid id, string account, string nickName) : base(id)
    {
        SetAccount(account);
        SetNickName(nickName);
    }

    /// <summary>
    /// 账号
    /// </summary>
    /// <value>The account.</value>
    public string Account { get; protected set; } = "";

    /// <summary>
    /// 昵称
    /// </summary>
    /// <value>The name of the nike.</value>
    public string NickName { get; protected set; } = "";

    /// <summary>
    /// 头像
    /// </summary>
    /// <value>The head icon.</value>
    public string Avatar { get; protected set; } = "";

    /// <summary>
    /// 手机号码
    /// </summary>
    /// <value>The phone number.</value>
    public string PhoneNumber { get; protected set; } = "";

    /// <summary>
    /// 手机号码是否已验证
    /// </summary>
    /// <value></value>
    public bool PhoneNumberVerified { get; protected set; }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; protected set; } = "";

    /// <summary>
    /// 电子邮箱是否已验证
    /// </summary>
    /// <value></value>
    public bool EmailVerified { get; protected set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; protected set; } = "";

    /// <summary>
    /// 用户密钥
    /// </summary>
    public string SecretKey { get; protected set; } = "";

    /// <summary>
    /// 允许登录时间开始
    /// </summary>
    public DateTime AllowStartTime { get; protected set; } = DateTime.UtcNow.Date.AddDays(-1);

    /// <summary>
    /// 允许登录时间结束
    /// </summary>
    public DateTime AllowEndTime { get; protected set; } = DateTime.UtcNow.Date.AddYears(100);

    /// <summary>
    /// 暂停用户开始日期
    /// </summary>
    public DateTime LockStartTime { get; protected set; } = DateTime.UtcNow.Date.AddYears(100);

    /// <summary>
    /// 暂停用户结束日期
    /// </summary>
    public DateTime LockEndDate { get; protected set; } = DateTime.UtcNow.Date.AddYears(100);

    /// <summary>
    /// 第一次访问时间
    /// </summary>
    public DateTime FirstVisitTime { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// 上一次访问时间
    /// </summary>
    public DateTime PreviousVisitTime { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// 最后访问时间
    /// </summary>
    public DateTime LastVisitTime { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// 最后修改密码日期
    /// </summary>
    public DateTime ChangPasswordDate { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// 个人简介
    /// </summary>
    [NotMapped]
    public string PersonalProfile => this.GetProperty<string>("PersonalProfile") ?? "";

    /// <summary>
    /// 国家/地区
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string Country => this.GetProperty<string>("Country") ?? "";

    /// <summary>
    /// 省
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string Province => this.GetProperty<string>("Province") ?? "";

    /// <summary>
    /// 市
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string City => this.GetProperty<string>("City") ?? "";

    /// <summary>
    /// 区
    /// </summary>
    [NotMapped]
    public string District => this.GetProperty<string>("District") ?? "";

    /// <summary>
    /// 街道地址
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string Address => this.GetProperty<string>("Address") ?? "";

    /// <summary>
    /// 电话区号
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string TelephoneNumberAreaCode => this.GetProperty<string>("TelephoneNumberAreaCode") ?? "";

    /// <summary>
    /// 固定电话
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string TelephoneNumber => this.GetProperty<string>("TelephoneNumber") ?? "";

    public bool IsDeleted { get; set; }
    
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
    public void SetPassword(string password)
    {
        Check.NotNullOrEmpty(password, nameof(password));
        if (!Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$"))
        {
            throw new VerifiedPasswordException(this.Id, password);
        }

        string passwordHash = Cryptography.PasswordStorage.CreateHash(password, out string secretKey);
        Password = passwordHash;
        SecretKey = secretKey;
        ChangPasswordDate = DateTime.UtcNow;
    }
    
    public void VerifyPassword(string password)
    {
        Check.NotNullOrEmpty(password, nameof(password));
        if (!Cryptography.PasswordStorage.VerifyPassword(password, Password, SecretKey))
        {
            throw new VerifyPasswordException(this.Id, password, Password, SecretKey);
        }
    }

    public void SetAccount(string account)
    {
        Check.NotNullOrWhiteSpace(account, nameof(account));
        Account = account;
    }
    
    public void SetNickName(string nickName)
    {
        Check.NotNullOrWhiteSpace(nickName, nameof(nickName));
        NickName = nickName;
    }
    
    public void SetAvatar(string avatar)
    {
        Check.NotNullOrWhiteSpace(avatar, nameof(avatar));
        Avatar = avatar;
    }
    
    public void SetPhoneNumber(string phoneNumber)
    {
        Check.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber));
        PhoneNumber = phoneNumber;
    }

    public void SetPhoneNumberVerified(bool phoneNumberVerified)
    {
        PhoneNumberVerified = phoneNumberVerified;
    }

    public void SetEmail(string email)
    {
        Check.NotNullOrWhiteSpace(email, nameof(email));
        Email = email;
    }
    
    public void SetEmailVerified(bool emailVerified)
    {
        EmailVerified = emailVerified;
    }
    
    public void SetPersonalProfile(string personalProfile)
    {
        this.SetProperty("PersonalProfile", personalProfile);
    }
    
    public void SetAddress(string country, string province, string city, string district, string address)
    {
        this.SetProperty("Country", country);
        this.SetProperty("Province", province);
        this.SetProperty("City", city);
        this.SetProperty("District", district);
        this.SetProperty("Address", address);
    }
    
    public void SetTelephoneNumber(string telephoneNumberAreaCode, string telephoneNumber)
    {
        this.SetProperty("TelephoneNumberAreaCode", telephoneNumberAreaCode);
        this.SetProperty("TelephoneNumber", telephoneNumber);
    }
    
    public void LockUser(DateTime startTime, DateTime endTime)
    {
        LockStartTime = startTime;
        LockEndDate = endTime;
    }

    public void AllowUser(DateTime startTime, DateTime endTime)
    {
        AllowStartTime = startTime;
        AllowEndTime = endTime;
    }

    public void CheckAllowTime()
    {
        DateTime now = DateTime.UtcNow;
        if (now < AllowStartTime || now > AllowEndTime)
        {
            throw new UserNotAllowLoginException(Id);
        }
    }

    public void CheckLockTime()
    {
        DateTime now = DateTime.UtcNow;
        if (now > LockStartTime && now < LockEndDate)
        {
            throw new UserLockLoginException(Id);
        }
    }
    
    public void AddRole(Guid id, Guid roleId)
    {
        UserRoles.Add(new UserRole(id, this.Id, roleId));
    }

    public void SetRoles(Dictionary<Guid, Guid> roleIds)
    {
        UserRoles.Clear();
        foreach (var item in roleIds)
        {
            UserRoles.Add(new UserRole(item.Key, this.Id, item.Value));
        }
    }
}