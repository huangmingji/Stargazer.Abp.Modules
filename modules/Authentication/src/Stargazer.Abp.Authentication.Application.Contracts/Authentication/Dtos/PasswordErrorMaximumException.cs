using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Stargazer.Abp.Authentication.Application.Contracts;

public class PasswordErrorMaximumException() : UserFriendlyException(message: "The password verification error has exceeded the maximum number of attempts.", 
    code: "PasswordErrorMaximum", details: "The password verification error has exceeded the maximum number of attempts.");