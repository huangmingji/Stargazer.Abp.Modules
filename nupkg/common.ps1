# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

$users = (
"modules/Users/src/Stargazer.Abp.Users.Domain.Shared",
"modules/Users/src/Stargazer.Abp.Users.Domain",
"modules/Users/src/Stargazer.Abp.Users.EntityFrameworkCore",
"modules/Users/src/Stargazer.Abp.Users.EntityFrameworkCore.DbMigrations",
"modules/Users/src/Stargazer.Abp.Users.Application.Contracts",
"modules/Users/src/Stargazer.Abp.Users.Application",
"modules/Users/src/Stargazer.Abp.Users.HttpApi",
"modules/Users/src/Stargazer.Abp.Users.HttpApi.Client"
)

$authentication = (
"modules/Authentication/src/Stargazer.Abp.Authentication.Domain.Shared",
"modules/Authentication/src/Stargazer.Abp.Authentication.Domain",
"modules/Authentication/src/Stargazer.Abp.Authentication.EntityFrameworkCore",
"modules/Authentication/src/Stargazer.Abp.Authentication.EntityFrameworkCore.DbMigrations",
"modules/Authentication/src/Stargazer.Abp.Authentication.Application.Contracts",
"modules/Authentication/src/Stargazer.Abp.Authentication.Application",
"modules/Authentication/src/Stargazer.Abp.Authentication.HttpApi",
"modules/Authentication/src/Stargazer.Abp.Authentication.HttpApi.Client"
)

$objectStorage = (
"modules/ObjectStorage/src/Stargazer.Abp.ObjectStorage.Application.Contracts",
"modules/ObjectStorage/src/Stargazer.Abp.ObjectStorage.Application",
"modules/ObjectStorage/src/Stargazer.Abp.ObjectStorage.HttpApi",
"modules/ObjectStorage/src/Stargazer.Abp.ObjectStorage.HttpApi.Client"
)

$wechat = (
"modules/Wechat/src/Stargazer.Abp.Wechat.Domain.Shared",
"modules/Wechat/src/Stargazer.Abp.Wechat.Domain",
"modules/Wechat/src/Stargazer.Abp.Wechat.EntityFrameworkCore",
"modules/Wechat/src/Stargazer.Abp.Wechat.EntityFrameworkCore.DbMigrations",
"modules/Wechat/src/Stargazer.Abp.Wechat.Application.Contracts",
"modules/Wechat/src/Stargazer.Abp.Wechat.Application",
"modules/Wechat/src/Stargazer.Abp.Wechat.HttpApi",
"modules/Wechat/src/Stargazer.Abp.Wechat.HttpApi.Client"
)

$captchat = (
"modules/Captchat/src/Stargazer.Abp.Captchat.HttpApi"
)

$projects = $users + $authentication + $objectStorage + $wechat + $captchat