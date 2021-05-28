using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add_TB_UploadReqYoutubeVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadReqYoutubeVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadReqYoutubeVideo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UploadReqYoutubeVideo_YoutubeVideo_ID",
                        column: x => x.ID,
                        principalTable: "YoutubeVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadReqYoutubeVideo");
        }
    }
}
