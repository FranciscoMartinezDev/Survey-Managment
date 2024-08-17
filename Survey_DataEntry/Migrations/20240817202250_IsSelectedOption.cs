using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_DataEntry.Migrations
{
    /// <inheritdoc />
    public partial class IsSelectedOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "QuestionOption",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "QuestionOption");
        }
    }
}
