using System;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat
{
	public interface IWechatUserService: IApplicationService
	{
		Task<WechatUserDataDto> GetAsync(Guid id);

		Task<WechatUserDataDto> FindAsync(string appId, string openId);

		Task<WechatUserDataDto> UpdateAsync(CreateOrUpdateWechatUserDataDto input);

		Task DeleteAsync(string appId, string openId);

		PagedResultDto<WechatUserDataDto> FindListAsync(PagedAndSortedResultRequestDto input);

    }
}

