using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainDatabase.Migrations
{
    public partial class InitfixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    IdSource = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceIdSource = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.IdSource);
                    table.ForeignKey(
                        name: "FK_Source_Source_SourceIdSource",
                        column: x => x.SourceIdSource,
                        principalTable: "Source",
                        principalColumn: "IdSource");
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    IdArticle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlToImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.IdArticle);
                    table.ForeignKey(
                        name: "FK_Article_Source_IdSource",
                        column: x => x.IdSource,
                        principalTable: "Source",
                        principalColumn: "IdSource",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_IdSource",
                table: "Article",
                column: "IdSource");

            migrationBuilder.CreateIndex(
                name: "IX_Source_SourceIdSource",
                table: "Source",
                column: "SourceIdSource");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Source");
        }
    }
}
