using System.Linq.Dynamic.Core.Tokenizer;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Stargazer.Abp.Wechat.Domain.Wechat;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Wechat.Application.Wechat
{
    public class WechatUserService : ApplicationService, IWechatUserService
    {
        private IRepository<WechatUserData, Guid> _repository;
        public WechatUserService(IRepository<WechatUserData, Guid> repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(string appId, string openId)
        {
           await _repository.DeleteAsync(x => x.AppId == appId && x.OpenId == openId);
        }

        public async Task<WechatUserDataDto> FindAsync(string appId, string openId)
        {
            var data = await _repository.FindAsync(x => x.AppId == appId && x.OpenId == openId);
            return ObjectMapper.Map<WechatUserData, WechatUserDataDto>(data);
        }

        public PagedResultDto<WechatUserDataDto> FindListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<WechatUserDataDto> GetAsync(Guid id)
        {
            var data = await _repository.GetAsync(id);
            return ObjectMapper.Map<WechatUserData, WechatUserDataDto>(data);
        }

        public async Task<WechatUserDataDto> UpdateAsync(CreateOrUpdateWechatUserDataDto input)
        {
            var data = await _repository.FindAsync(x => x.AppId == input.AppId && x.OpenId == input.OpenId);
            if (data == null)
            {
                data = ObjectMapper.Map<CreateOrUpdateWechatUserDataDto, WechatUserData>(input);
                data.SetId(GuidGenerator.Create());
            }
            else
            {
                data.SetData(nickName: input.NickName, subscribe: input.Subscribe, sex: input.Sex, country: input.Country,
                    province: input.Province, city: input.City, headImgUrl: input.HeadImgUrl, SubscribeTime: input.SubscribeTime,
                    unionid: input.Unionid, remark: input.Remark, groupId: input.GroupId, tagidList: input.TagidList);
            }
            return ObjectMapper.Map<WechatUserData, WechatUserDataDto>(data);
        }
    }
}

