using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PCBuilder.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionEntity",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionEntity", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissionEntity_PermissionEntity_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionEntity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleEntity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleEntity", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoleEntity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PermissionEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ReadPcItems" },
                    { 2, "CreatePcItems" },
                    { 3, "UpdatePcItems" },
                    { 4, "DeletePcItems" },
                    { 5, "ReadPc" },
                    { 6, "CreatePc" },
                    { 7, "UpdatePc" },
                    { 8, "DeletePc" },
                    { 9, "ScheduleDate" },
                    { 10, "CancelScheduleDate" },
                    { 11, "RescheduleDate" },
                    { 12, "GetUsersScheduleDate" },
                    { 13, "GetMastersScheduleDate" },
                    { 14, "GetAllScheduleDates" },
                    { 15, "PayOrder" },
                    { 16, "GetUsersOrders" },
                    { 17, "GetMastersOrders" },
                    { 18, "GetAllOrders" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Master" },
                    { 3, "Manager" },
                    { 4, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionEntity",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 5, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 1, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 10, 2 },
                    { 11, 2 },
                    { 13, 2 },
                    { 17, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 3 },
                    { 9, 3 },
                    { 10, 3 },
                    { 11, 3 },
                    { 12, 3 },
                    { 13, 3 },
                    { 14, 3 },
                    { 16, 3 },
                    { 17, 3 },
                    { 18, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 },
                    { 5, 4 },
                    { 6, 4 },
                    { 7, 4 },
                    { 8, 4 },
                    { 9, 4 },
                    { 10, 4 },
                    { 11, 4 },
                    { 12, 4 },
                    { 13, 4 },
                    { 14, 4 },
                    { 15, 4 },
                    { 16, 4 },
                    { 17, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionEntity_PermissionId",
                table: "RolePermissionEntity",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleEntity_RoleId",
                table: "UserRoleEntity",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionEntity");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

            migrationBuilder.DropTable(
                name: "PermissionEntity");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
