using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebsiteStatusChecker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UptimeChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WebsiteId = table.Column<int>(type: "integer", nullable: false),
                    CheckTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    ResponseBody = table.Column<string>(type: "text", nullable: true),
                    IsUp = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UptimeChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UptimeChecks_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UptimeChecks_WebsiteId",
                table: "UptimeChecks",
                column: "WebsiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UptimeChecks");

            migrationBuilder.DropTable(
                name: "Websites");
        }
    }
}
