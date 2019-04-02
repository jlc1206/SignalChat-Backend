using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalChat.Migrations
{
    public partial class ChannelGroupsAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Channels_ChannelID",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Channels_ChannelID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChannelID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ChannelID",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ChannelID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChannelID",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ChannelGroups",
                columns: table => new
                {
                    ChannelID = table.Column<int>(nullable: false),
                    GroupID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelGroups", x => new { x.ChannelID, x.GroupID });
                    table.ForeignKey(
                        name: "FK_ChannelGroups_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelGroups_AspNetRoles_GroupID",
                        column: x => x.GroupID,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelUsers",
                columns: table => new
                {
                    ChannelID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelUsers", x => new { x.ChannelID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ChannelUsers_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelUsers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelGroups_GroupID",
                table: "ChannelGroups",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelUsers_UserID",
                table: "ChannelUsers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelGroups");

            migrationBuilder.DropTable(
                name: "ChannelUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "ChannelID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChannelID",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChannelID",
                table: "AspNetUsers",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ChannelID",
                table: "AspNetRoles",
                column: "ChannelID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Channels_ChannelID",
                table: "AspNetRoles",
                column: "ChannelID",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Channels_ChannelID",
                table: "AspNetUsers",
                column: "ChannelID",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
