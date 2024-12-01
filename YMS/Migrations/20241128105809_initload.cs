using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YMS.Migrations
{
    /// <inheritdoc />
    public partial class initload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coils",
                columns: table => new
                {
                    CoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CoilBarcodeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Diameter = table.Column<float>(type: "real", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentLocationID = table.Column<int>(type: "int", nullable: true),
                    LastMoved = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coils", x => x.CoilID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Yards",
                columns: table => new
                {
                    YardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CurrentOccupancy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yards", x => x.YardID);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Shift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoragePlaceholders",
                columns: table => new
                {
                    PlaceHolderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YardID = table.Column<int>(type: "int", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    OccupyingCoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaxWeightCapacity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePlaceholders", x => x.PlaceHolderID);
                    table.ForeignKey(
                        name: "FK_StoragePlaceholders_Coils_OccupyingCoilID",
                        column: x => x.OccupyingCoilID,
                        principalTable: "Coils",
                        principalColumn: "CoilID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoragePlaceholders_Yards_YardID",
                        column: x => x.YardID,
                        principalTable: "Yards",
                        principalColumn: "YardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    AuditID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PerformedByUserID = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_AuditTrails_Coils_CoilID",
                        column: x => x.CoilID,
                        principalTable: "Coils",
                        principalColumn: "CoilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditTrails_Users_PerformedByUserID",
                        column: x => x.PerformedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    InspectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InspectorID = table.Column<int>(type: "int", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Diameter = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    VisualCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePaths = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.InspectionID);
                    table.ForeignKey(
                        name: "FK_Inspections_Coils_CoilID",
                        column: x => x.CoilID,
                        principalTable: "Coils",
                        principalColumn: "CoilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inspections_Users_InspectorID",
                        column: x => x.InspectorID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutgoingDispatches",
                columns: table => new
                {
                    DispatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DispatchedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispatchedByUserID = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutgoingDispatches", x => x.DispatchID);
                    table.ForeignKey(
                        name: "FK_OutgoingDispatches_Coils_CoilID",
                        column: x => x.CoilID,
                        principalTable: "Coils",
                        principalColumn: "CoilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutgoingDispatches_Users_DispatchedByUserID",
                        column: x => x.DispatchedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoilMovements",
                columns: table => new
                {
                    MovementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoilID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToPlaceHolderID = table.Column<int>(type: "int", nullable: false),
                    FromPlaceHolderID = table.Column<int>(type: "int", nullable: false),
                    MovedByUserID = table.Column<int>(type: "int", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoilMovements", x => x.MovementID);
                    table.ForeignKey(
                        name: "FK_CoilMovements_Coils_CoilID",
                        column: x => x.CoilID,
                        principalTable: "Coils",
                        principalColumn: "CoilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoilMovements_StoragePlaceholders_FromPlaceHolderID",
                        column: x => x.FromPlaceHolderID,
                        principalTable: "StoragePlaceholders",
                        principalColumn: "PlaceHolderID");
                    table.ForeignKey(
                        name: "FK_CoilMovements_StoragePlaceholders_ToPlaceHolderID",
                        column: x => x.ToPlaceHolderID,
                        principalTable: "StoragePlaceholders",
                        principalColumn: "PlaceHolderID");
                    table.ForeignKey(
                        name: "FK_CoilMovements_Users_MovedByUserID",
                        column: x => x.MovedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator with full access", "Admin" },
                    { 2, "Manager with limited access", "Manager" },
                    { 3, "Supervisor with operational access", "Supervisor" },
                    { 4, "Operator with basic access", "Operator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_CoilID",
                table: "AuditTrails",
                column: "CoilID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_PerformedByUserID",
                table: "AuditTrails",
                column: "PerformedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CoilMovements_CoilID",
                table: "CoilMovements",
                column: "CoilID");

            migrationBuilder.CreateIndex(
                name: "IX_CoilMovements_FromPlaceHolderID",
                table: "CoilMovements",
                column: "FromPlaceHolderID");

            migrationBuilder.CreateIndex(
                name: "IX_CoilMovements_MovedByUserID",
                table: "CoilMovements",
                column: "MovedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CoilMovements_ToPlaceHolderID",
                table: "CoilMovements",
                column: "ToPlaceHolderID");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_CoilID",
                table: "Inspections",
                column: "CoilID");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_InspectorID",
                table: "Inspections",
                column: "InspectorID");

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingDispatches_CoilID",
                table: "OutgoingDispatches",
                column: "CoilID");

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingDispatches_DispatchedByUserID",
                table: "OutgoingDispatches",
                column: "DispatchedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleID",
                table: "RolePermissions",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlaceholders_OccupyingCoilID",
                table: "StoragePlaceholders",
                column: "OccupyingCoilID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlaceholders_YardID",
                table: "StoragePlaceholders",
                column: "YardID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "CoilMovements");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "OutgoingDispatches");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StoragePlaceholders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Coils");

            migrationBuilder.DropTable(
                name: "Yards");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
