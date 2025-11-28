using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stargazer.Abp.Authentication.EntityFrameworkCore.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<string>(type: "text", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    DeviceType = table.Column<int>(type: "integer", nullable: false),
                    DeviceOS = table.Column<string>(type: "text", nullable: false),
                    DeviceOSVersion = table.Column<string>(type: "text", nullable: false),
                    DeviceBrowser = table.Column<string>(type: "text", nullable: false),
                    DeviceBrowserVersion = table.Column<string>(type: "text", nullable: false),
                    DeviceIp = table.Column<string>(type: "text", nullable: false),
                    DeviceLocation = table.Column<string>(type: "text", nullable: false),
                    DeviceUserAgent = table.Column<string>(type: "text", nullable: false),
                    PushToken = table.Column<string>(type: "text", nullable: false),
                    LastActivityTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsTrusted = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: true),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    RefreshTokenExpires = table.Column<long>(type: "bigint", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    GeoLocation = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsExclusive = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessions_UserDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "UserDevices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_DeviceId",
                table: "UserSessions",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "UserDevices");
        }
    }
}
