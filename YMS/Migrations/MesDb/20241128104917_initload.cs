using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YMS.Migrations.MesDb
{
    /// <inheritdoc />
    public partial class initload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MESCoils",
                columns: table => new
                {
                    CoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CoilBarcodeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Diameter = table.Column<float>(type: "real", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESCoils", x => x.CoilID);
                });

            migrationBuilder.CreateTable(
                name: "MESIncomingCoils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoilID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESIncomingCoils", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MESCoils");

            migrationBuilder.DropTable(
                name: "MESIncomingCoils");
        }
    }
}
