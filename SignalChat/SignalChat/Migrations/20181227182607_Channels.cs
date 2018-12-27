using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalChat.Migrations
{
    public partial class Channels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SignalChatUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Channels_AspNetUsers_SignalChatUserId",
                        column: x => x.SignalChatUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    SignalChatUserID = table.Column<string>(nullable: true),
                    ChannelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SignalChatUserID",
                        column: x => x.SignalChatUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_SignalChatUserId",
                table: "Channels",
                column: "SignalChatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChannelID",
                table: "Messages",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SignalChatUserID",
                table: "Messages",
                column: "SignalChatUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Channels");
        }
    }
}
