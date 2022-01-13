using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FARO.Manager3d.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nominal_point",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<float>(type: "real", nullable: false),
                    Y = table.Column<float>(type: "real", nullable: false),
                    Z = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nominal_point", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "actual_point",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nominal_point_id = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<float>(type: "real", nullable: false),
                    Y = table.Column<float>(type: "real", nullable: false),
                    Z = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actual_point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actual_point_nominal_point_nominal_point_id",
                        column: x => x.nominal_point_id,
                        principalTable: "nominal_point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actual_point_nominal_point_id",
                table: "actual_point",
                column: "nominal_point_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actual_point");

            migrationBuilder.DropTable(
                name: "nominal_point");
        }
    }
}
