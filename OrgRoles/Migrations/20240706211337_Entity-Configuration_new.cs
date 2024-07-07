using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgRoles.Migrations
{
    /// <inheritdoc />
    public partial class EntityConfigurationnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_ParentId",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_PARENT",
                table: "Roles",
                column: "ParentId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_PARENT",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_ParentId",
                table: "Roles",
                column: "ParentId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
