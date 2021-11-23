using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingeco.Intranet.Migrations
{
    public partial class RemovedCompositeKeyFromWebMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                columns: new[] { "Id", "ViewId" });
        }
    }
}
