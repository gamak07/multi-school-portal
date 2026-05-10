using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiPortalSchoolSys.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPublishedToStudentResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "StudentResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "StudentResults");
        }
    }
}
