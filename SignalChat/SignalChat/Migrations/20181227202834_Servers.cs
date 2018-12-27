using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalChat.Migrations
{
    public partial class Servers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_AspNetUsers_SignalChatUserId",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_SignalChatUserId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "SignalChatUserId",
                table: "Channels");

            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "Channels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "roleRestricted",
                table: "Channels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "userRestricted",
                table: "Channels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ChannelID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentChannel",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentServer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChannelID",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_ServerID",
                table: "Channels",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChannelID",
                table: "AspNetUsers",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ChannelID",
                table: "AspNetRoles",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ServerID",
                table: "AspNetRoles",
                column: "ServerID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Channels_ChannelID",
                table: "AspNetRoles",
                column: "ChannelID",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Servers_ServerID",
                table: "AspNetRoles",
                column: "ServerID",
                principalTable: "Servers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Channels_ChannelID",
                table: "AspNetUsers",
                column: "ChannelID",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Servers_ServerID",
                table: "Channels",
                column: "ServerID",
                principalTable: "Servers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Channels_ChannelID",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Servers_ServerID",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Channels_ChannelID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Servers_ServerID",
                table: "Channels");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Channels_ServerID",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChannelID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ChannelID",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ServerID",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "roleRestricted",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "userRestricted",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "ChannelID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentChannel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentServer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChannelID",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "SignalChatUserId",
                table: "Channels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_SignalChatUserId",
                table: "Channels",
                column: "SignalChatUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_AspNetUsers_SignalChatUserId",
                table: "Channels",
                column: "SignalChatUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
