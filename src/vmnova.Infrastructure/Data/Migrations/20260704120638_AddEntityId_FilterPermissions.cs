using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vmnova.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityId_FilterPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_DomainPermissions_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_DomainRoles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_DomainRoles_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_DomainUsers_UserId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "DomainUserRoles");

            migrationBuilder.RenameTable(
                name: "RolePermission",
                newName: "DomainRolePermissions");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                table: "DomainUserRoles",
                newName: "IX_DomainUserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_RoleId",
                table: "DomainRolePermissions",
                newName: "IX_DomainRolePermissions_RoleId");

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "DomainPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainUserRoles",
                table: "DomainUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainRolePermissions",
                table: "DomainRolePermissions",
                columns: new[] { "PermissionId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DomainRolePermissions_DomainPermissions_PermissionId",
                table: "DomainRolePermissions",
                column: "PermissionId",
                principalTable: "DomainPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DomainRolePermissions_DomainRoles_RoleId",
                table: "DomainRolePermissions",
                column: "RoleId",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DomainUserRoles_DomainRoles_RoleId",
                table: "DomainUserRoles",
                column: "RoleId",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DomainUserRoles_DomainUsers_UserId",
                table: "DomainUserRoles",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainRolePermissions_DomainPermissions_PermissionId",
                table: "DomainRolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_DomainRolePermissions_DomainRoles_RoleId",
                table: "DomainRolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_DomainUserRoles_DomainRoles_RoleId",
                table: "DomainUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_DomainUserRoles_DomainUsers_UserId",
                table: "DomainUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainUserRoles",
                table: "DomainUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainRolePermissions",
                table: "DomainRolePermissions");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "DomainPermissions");

            migrationBuilder.RenameTable(
                name: "DomainUserRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "DomainRolePermissions",
                newName: "RolePermission");

            migrationBuilder.RenameIndex(
                name: "IX_DomainUserRoles_UserId",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DomainRolePermissions_RoleId",
                table: "RolePermission",
                newName: "IX_RolePermission_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_DomainPermissions_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "DomainPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_DomainRoles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_DomainRoles_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_DomainUsers_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
