using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodieBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AdminMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MenuIcon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ParentMenuId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MenuOrder = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    ModifierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminMenu_AdminMenu",
                        column: x => x.ParentMenuId,
                        principalTable: "AdminMenu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuAuthorization",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    InsertAuthorization = table.Column<bool>(type: "bit", nullable: true),
                    UpdateAuthorization = table.Column<bool>(type: "bit", nullable: true),
                    DeleteAuthorization = table.Column<bool>(type: "bit", nullable: true),
                    SelectAuthorization = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    ModifierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    ModifierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    ModifierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenu_ParentMenuId",
                table: "AdminMenu",
                column: "ParentMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMenu");

            migrationBuilder.DropTable(
                name: "MenuAuthorization");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PublicationDate",
                table: "Posts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ModifiedDate",
                table: "Posts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
