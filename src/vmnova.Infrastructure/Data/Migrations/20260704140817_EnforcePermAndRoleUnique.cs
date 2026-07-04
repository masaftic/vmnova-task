using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vmnova.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnforcePermAndRoleUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DomainRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DomainPermissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DomainRoles_Name",
                table: "DomainRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DomainPermissions_Name",
                table: "DomainPermissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DomainRoles_Name",
                table: "DomainRoles");

            migrationBuilder.DropIndex(
                name: "IX_DomainPermissions_Name",
                table: "DomainPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DomainRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DomainPermissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
