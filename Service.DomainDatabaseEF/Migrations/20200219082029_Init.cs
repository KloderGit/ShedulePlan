using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Service.DomainDatabaseEF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Domain");

            migrationBuilder.EnsureSchema(
                name: "SecondScheme");

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Domain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Draft = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Anotherdb",
                schema: "SecondScheme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anotherdb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitLayouts",
                schema: "Domain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitLayouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitLayouts_UnitLayouts_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Domain",
                        principalTable: "UnitLayouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitLayouts_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Domain",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitParams",
                schema: "Domain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    UnitLayoutId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitParams_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Domain",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitParams_UnitLayouts_UnitLayoutId",
                        column: x => x.UnitLayoutId,
                        principalSchema: "Domain",
                        principalTable: "UnitLayouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitLayouts_ParentId",
                schema: "Domain",
                table: "UnitLayouts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitLayouts_UnitId",
                schema: "Domain",
                table: "UnitLayouts",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitParams_UnitId",
                schema: "Domain",
                table: "UnitParams",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitParams_UnitLayoutId",
                schema: "Domain",
                table: "UnitParams",
                column: "UnitLayoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitParams",
                schema: "Domain");

            migrationBuilder.DropTable(
                name: "Anotherdb",
                schema: "SecondScheme");

            migrationBuilder.DropTable(
                name: "UnitLayouts",
                schema: "Domain");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Domain");
        }
    }
}
