using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO_User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFieldCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Jobs",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Jobs",
                newName: "CreatedById");
        }
    }
}
