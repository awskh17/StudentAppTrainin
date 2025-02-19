using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnsureUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "age",
                table: "Students",
                newName: "Age");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Students",
                newName: "age");
        }
    }
}
