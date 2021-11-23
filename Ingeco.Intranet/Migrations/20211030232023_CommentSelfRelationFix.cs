using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingeco.Intranet.Migrations
{
    public partial class CommentSelfRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_RepliedToId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "RepliedToId",
                table: "Comments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_RepliedToId",
                table: "Comments",
                column: "RepliedToId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_RepliedToId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "RepliedToId",
                table: "Comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_RepliedToId",
                table: "Comments",
                column: "RepliedToId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
