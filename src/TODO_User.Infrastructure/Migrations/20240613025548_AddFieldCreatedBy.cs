using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO_User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Jobs");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
