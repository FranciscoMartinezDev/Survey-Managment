using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_DataEntry.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "Survey",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurveyCode",
                table: "Survey",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "SurveyCode",
                table: "Survey");
        }
    }
}
