using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class ddsda212 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentHistory_Readers_Id_Reader",
                table: "RentHistory");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RentHistory_Id_Reader",
                table: "RentHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id_Role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id_Role);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Role = table.Column<int>(type: "int", nullable: true),
                    Date_Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id_User);
                    table.ForeignKey(
                        name: "FK_Readers_Roles_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Roles",
                        principalColumn: "Id_Role");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_Id_Reader",
                table: "RentHistory",
                column: "Id_Reader");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_Id_Role",
                table: "Readers",
                column: "Id_Role");

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistory_Readers_Id_Reader",
                table: "RentHistory",
                column: "Id_Reader",
                principalTable: "Readers",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
