using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalChat.Migrations
{
    public partial class RemoveServers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Servers_ServerID",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Servers_ServerID",
                table: "Channels");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Channels_ServerID",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ServerID",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "CurrentServer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "Channels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentServer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_AspNetRoles_ServerID",
                table: "AspNetRoles",
                column: "ServerID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Servers_ServerID",
                table: "AspNetRoles",
                column: "ServerID",
                principalTable: "Servers",
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
    }
}
