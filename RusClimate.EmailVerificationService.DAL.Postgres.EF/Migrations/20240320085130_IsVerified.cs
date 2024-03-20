using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RusClimate.EmailVerificationService.DAL.Postgres.EF.Migrations
{
    /// <inheritdoc />
    public partial class IsVerified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Emails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Emails");
        }
    }
}
