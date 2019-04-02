using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalChat.Migrations
{
    public partial class Improved_Channel_User_Group_association : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roleRestricted",
                table: "Channels");

            migrationBuilder.RenameColumn(
                name: "userRestricted",
                table: "Channels",
                newName: "EnableWhitelist");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ChannelUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isJoined",
                table: "ChannelUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChannelUsers");

            migrationBuilder.DropColumn(
                name: "isJoined",
                table: "ChannelUsers");

            migrationBuilder.RenameColumn(
                name: "EnableWhitelist",
                table: "Channels",
                newName: "userRestricted");

            migrationBuilder.AddColumn<bool>(
                name: "roleRestricted",
                table: "Channels",
                nullable: false,
                defaultValue: false);
        }
    }
}
