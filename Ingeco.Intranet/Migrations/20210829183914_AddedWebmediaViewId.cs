using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingeco.Intranet.Migrations
{
    public partial class AddedWebmediaViewId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.AddColumn<int>(
                name: "ViewId",
                table: "Media",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                columns: new[] { "Id", "ViewId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ViewId",
                table: "Media");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");
        }
    }
}
