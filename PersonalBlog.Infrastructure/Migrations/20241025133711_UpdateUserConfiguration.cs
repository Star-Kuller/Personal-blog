using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AccountName",
                table: "Users");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_AccountName",
                table: "Users",
                column: "AccountName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_AccountName",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountName",
                table: "Users",
                column: "AccountName",
                unique: true);
        }
    }
}
