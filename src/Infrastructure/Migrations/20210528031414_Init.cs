using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppMail = table.Column<bool>(type: "bit", nullable: false),
                    StorageID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppReqMail",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppReqMail", x => new { x.ID, x.Token });
                });

            migrationBuilder.CreateTable(
                name: "ChangeReqPassword",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeReqPassword", x => new { x.ID, x.AccountID });
                });

            migrationBuilder.CreateTable(
                name: "VideoInfo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeVideoStatistics",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoutubeVideoID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideoStatistics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_YoutubeVideoStatistics_YoutubeVideo_YoutubeVideoID",
                        column: x => x.YoutubeVideoID,
                        principalTable: "YoutubeVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Mail",
                table: "Account",
                column: "Mail");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeReqPassword_Code",
                table: "ChangeReqPassword",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideoStatistics_YoutubeVideoID",
                table: "YoutubeVideoStatistics",
                column: "YoutubeVideoID");

            //フルテキストインデックスを作成
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG YoutubeVideo_Catalog ON FILEGROUP SECONDARY",
                suppressTransaction: true
            );
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT INDEX ON YoutubeVideo (VideoTitle, ChanelTitle) KEY INDEX PK_YoutubeVideo ON YoutubeVideo_Catalog",
                suppressTransaction: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AppReqMail");

            migrationBuilder.DropTable(
                name: "ChangeReqPassword");

            migrationBuilder.DropTable(
                name: "VideoInfo");

            migrationBuilder.DropTable(
                name: "YoutubeVideoStatistics");

            migrationBuilder.DropTable(
                name: "YoutubeVideo");
        }
    }
}
