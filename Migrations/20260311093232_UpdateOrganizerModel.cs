using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventageApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrganizerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Organizers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Organizers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
