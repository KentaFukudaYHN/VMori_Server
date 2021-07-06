using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class deleteStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutsourceVideoStatistics");

            migrationBuilder.DropIndex(
                name: "IX_OutsourceVideo_VideoId",
                table: "OutsourceVideo");

            migrationBuilder.AddColumn<decimal>(
                name: "CommentCount",
                table: "UploadReqOutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LikeCount",
                table: "UploadReqOutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ViewCount",
                table: "UploadReqOutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommentCount",
                table: "OutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LikeCount",
                table: "OutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ViewCount",
                table: "OutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount",
                table: "OutsourceVideo",
                columns: new[] { "VideoId", "ViewCount", "CommentCount", "LikeCount" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount",
                table: "OutsourceVideo");

            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "UploadReqOutsourceVideo");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "UploadReqOutsourceVideo");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "UploadReqOutsourceVideo");

            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "OutsourceVideo");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "OutsourceVideo");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "OutsourceVideo");

            migrationBuilder.CreateTable(
                name: "OutsourceVideoStatistics",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    OutsourceVideoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpReqOutsourceVideoID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsourceVideoStatistics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OutsourceVideoStatistics_OutsourceVideo_OutsourceVideoId",
                        column: x => x.OutsourceVideoId,
                        principalTable: "OutsourceVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutsourceVideoStatistics_UploadReqOutsourceVideo_UpReqOutsourceVideoID",
                        column: x => x.UpReqOutsourceVideoID,
                        principalTable: "UploadReqOutsourceVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideo_VideoId",
                table: "OutsourceVideo",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideoStatistics_OutsourceVideoId",
                table: "OutsourceVideoStatistics",
                column: "OutsourceVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideoStatistics_UpReqOutsourceVideoID",
                table: "OutsourceVideoStatistics",
                column: "UpReqOutsourceVideoID");

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideoStatistics_VideoId",
                table: "OutsourceVideoStatistics",
                column: "VideoId");
        }
    }
}
