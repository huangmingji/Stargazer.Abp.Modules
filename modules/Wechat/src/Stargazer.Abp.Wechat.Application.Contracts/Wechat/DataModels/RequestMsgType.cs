namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;

public enum RequestMsgType {
    Text = 0,

    Location = 1,

    Image = 2,

    Voice = 3,

    Video = 4,

    Link = 5,

    Event = 6,

    ShortVideo = 7,
}