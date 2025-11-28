namespace Stargazer.Abp.Users.Application.Contracts
{
    public class UpdatePersonalSettingsDto
    {
        public string Name { get; set; } = "";

        public string PersonalProfile { get; set; } = "";

        public string Country { get; set; } = "";

        public string Province { get; set; } = "";

        public string City { get; set; } = "";

        public string District { get; set; } = "";

        public string Address { get; set; } = "";

        public string TelephoneNumberAreaCode { get; set; } = "";

        public string TelephoneNumber { get; set; } = "";
    }
}