using System;
namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels
{
	public class OpenPlatform
	{
        public string AppId { get; set; } = "";

        public string AppSecret { get; set; } = "";

        public string Token { get; set; } = "";

        public string AesKey { get; set; } = "";
    }
}

