using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgRoles.Migrations
{
    /// <inheritdoc />
    public partial class Postgresconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    isCandidate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLE_PARENT",
                        column: x => x.ParentId,
                        principalTable: "Roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ParentId",
                table: "Roles",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
