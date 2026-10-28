using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sgcae.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CredentialCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRecords_AccessPoints_AccessPointId",
                        column: x => x.AccessPointId,
                        principalTable: "AccessPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessRecords_Credentials_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccessPoints",
                columns: new[] { "Id", "CreatedAt", "IsEnabled", "Location", "Name", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0199f7a0-0000-7000-8000-000000000001"), new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, "Primer piso, entrada principal", "Recepción principal", "Reception", null },
                    { new Guid("0199f7a0-0000-7000-8000-000000000002"), new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, "Sótano, acceso vehicular", "Parqueadero", "Parking", null },
                    { new Guid("0199f7a0-0000-7000-8000-000000000003"), new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, "Zona de carga y descarga", "Bodega central", "Warehouse", null },
                    { new Guid("0199f7a0-0000-7000-8000-000000000004"), new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, "Segundo piso, área de tecnología", "Sala de servidores", "RestrictedArea", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessPoints_Name",
                table: "AccessPoints",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_AccessPointId_OccurredAt",
                table: "AccessRecords",
                columns: new[] { "AccessPointId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_CredentialId",
                table: "AccessRecords",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_EmployeeId_OccurredAt",
                table: "AccessRecords",
                columns: new[] { "EmployeeId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_OccurredAt",
                table: "AccessRecords",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_Result",
                table: "AccessRecords",
                column: "Result");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_Code",
                table: "Credentials",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_EmployeeId_Status",
                table: "Credentials",
                columns: new[] { "EmployeeId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Area",
                table: "Employees",
                column: "Area");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdentificationNumber",
                table: "Employees",
                column: "IdentificationNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRecords");

            migrationBuilder.DropTable(
                name: "AccessPoints");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
