using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiPortalSchoolSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSpuriousParentId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ParentId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentId1",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentId1",
                table: "Students",
                column: "ParentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentId1",
                table: "Students",
                column: "ParentId1",
                principalTable: "Parents",
                principalColumn: "Id");
        }
    }
}
