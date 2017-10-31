using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace test.Migrations
{
    public partial class DbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accaunts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accaunts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wokers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccauntId = table.Column<int>(type: "INTEGER", nullable: false),
                    Begin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Rate = table.Column<int>(type: "INTEGER", nullable: false),
                    ShefId = table.Column<int>(type: "INTEGER", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wokers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wokers_Accaunts_AccauntId",
                        column: x => x.AccauntId,
                        principalTable: "Accaunts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wokers_Wokers_ShefId",
                        column: x => x.ShefId,
                        principalTable: "Wokers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wokers_AccauntId",
                table: "Wokers",
                column: "AccauntId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wokers_ShefId",
                table: "Wokers",
                column: "ShefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wokers");

            migrationBuilder.DropTable(
                name: "Accaunts");
        }
    }
}
