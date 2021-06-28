using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddVideoComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutsouceVideoChannel",
                table: "OutsouceVideoChannel");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "OutsouceVideoChannel");

            migrationBuilder.RenameTable(
                name: "OutsouceVideoChannel",
                newName: "OutsourceVideoChannel");

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "OutsourceVideoStatistics",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "OutsourceVideo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelId",
                table: "ChannelTransition",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutsourceVideoChannel",
                table: "OutsourceVideoChannel",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "VideoComment",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideoComment_OutsourceVideo_VideoId",
                        column: x => x.VideoId,
                        principalTable: "OutsourceVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideoStatistics_VideoId",
                table: "OutsourceVideoStatistics",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideo_VideoId",
                table: "OutsourceVideo",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelTransition_ChannelId",
                table: "ChannelTransition",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoComment_VideoId",
                table: "VideoComment",
                column: "VideoId");

            //フルテキストインデックスを作成
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG Video_Catalog ON FILEGROUP SECONDARY",
                suppressTransaction: true
            );
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT INDEX ON OutsourceVideo (VideoTitle, ChanelTitle, Description, TagsData) KEY INDEX PK_OutsourceVideo ON Video_Catalog",
                suppressTransaction: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoComment");

            migrationBuilder.DropIndex(
                name: "IX_OutsourceVideoStatistics_VideoId",
                table: "OutsourceVideoStatistics");

            migrationBuilder.DropIndex(
                name: "IX_OutsourceVideo_VideoId",
                table: "OutsourceVideo");

            migrationBuilder.DropIndex(
                name: "IX_ChannelTransition_ChannelId",
                table: "ChannelTransition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OutsourceVideoChannel",
                table: "OutsourceVideoChannel");

            migrationBuilder.RenameTable(
                name: "OutsourceVideoChannel",
                newName: "OutsouceVideoChannel");

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "OutsourceVideoStatistics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "OutsourceVideo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelId",
                table: "ChannelTransition",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChannelId",
                table: "OutsouceVideoChannel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutsouceVideoChannel",
                table: "OutsouceVideoChannel",
                column: "ID");
        }
    }
}
