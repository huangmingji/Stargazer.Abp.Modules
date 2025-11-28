namespace Stargazer.Abp.Users.Application.Contracts
{
    public class CreateOrUpdateUserWithRolesDto : CreateUserDto
    {
        public string Account { get; set; } = "";

        /// <summary>
        /// 电子邮箱是否已验证
        /// </summary>
        /// <value></value>
        public bool EmailVerified { get; set; }
        
        public string PhoneNumber { get; set; } = "";
        
        /// <summary>
        /// 手机号码是否已验证
        /// </summary>
        /// <value></value>
        public bool PhoneNumberVerified { get; set; }
        
        /// <summary>
        /// 允许登录时间开始
        /// </summary>
        public DateTime AllowStartTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 允许登录时间结束
        /// </summary>
        public DateTime AllowEndTime { get; set; } = DateTime.UtcNow.AddYears(100);
        

        /// <summary>
        /// 暂停用户开始日期
        /// </summary>
        public DateTime LockStartTime { get; set; } = DateTime.UtcNow.AddYears(100);

        /// <summary>
        /// 暂停用户结束日期
        /// </summary>
        public DateTime LockEndDate { get; set; } = DateTime.UtcNow.AddYears(100);

        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}