using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace buses.Migrations
{
    /// <inheritdoc />
    public partial class migracionIncial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "buses",
                columns: table => new
                {
                    IdBus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoBus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kilometraje = table.Column<int>(type: "int", nullable: false),
                    EsHabilitado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdBus);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chofer",
                columns: table => new
                {
                    IdChofer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kilometraje = table.Column<int>(type: "int", nullable: false),
                    EsHabilitado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdChofer);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tramo",
                columns: table => new
                {
                    IdTramo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Origen = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Destino = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Distancia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdTramo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "viaje",
                columns: table => new
                {
                    IdViaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TramoId = table.Column<int>(type: "int", nullable: false),
                    TramoIdTramo = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    BusIdBus = table.Column<int>(type: "int", nullable: false),
                    ChoferId = table.Column<int>(type: "int", nullable: false),
                    ChoferIdChofer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdViaje);
                    table.ForeignKey(
                        name: "FK_Viaje_Bus",
                        column: x => x.BusId,
                        principalTable: "buses",
                        principalColumn: "IdBus",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Viaje_Chofer",
                        column: x => x.ChoferId,
                        principalTable: "chofer",
                        principalColumn: "IdChofer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Viaje_Tramo",
                        column: x => x.TramoId,
                        principalTable: "tramo",
                        principalColumn: "IdTramo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_viaje_buses_BusIdBus",
                        column: x => x.BusIdBus,
                        principalTable: "buses",
                        principalColumn: "IdBus",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_viaje_chofer_ChoferIdChofer",
                        column: x => x.ChoferIdChofer,
                        principalTable: "chofer",
                        principalColumn: "IdChofer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_viaje_tramo_TramoIdTramo",
                        column: x => x.TramoIdTramo,
                        principalTable: "tramo",
                        principalColumn: "IdTramo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistroViajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdViaje = table.Column<int>(type: "int", nullable: false),
                    IdBus = table.Column<int>(type: "int", nullable: false),
                    IdChofer = table.Column<int>(type: "int", nullable: false),
                    IdTramo = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BusIdBus = table.Column<int>(type: "int", nullable: false),
                    ChoferIdChofer = table.Column<int>(type: "int", nullable: false),
                    TramoIdTramo = table.Column<int>(type: "int", nullable: false),
                    ViajeIdViaje = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroViajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroViajes_buses_BusIdBus",
                        column: x => x.BusIdBus,
                        principalTable: "buses",
                        principalColumn: "IdBus",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroViajes_chofer_ChoferIdChofer",
                        column: x => x.ChoferIdChofer,
                        principalTable: "chofer",
                        principalColumn: "IdChofer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroViajes_tramo_TramoIdTramo",
                        column: x => x.TramoIdTramo,
                        principalTable: "tramo",
                        principalColumn: "IdTramo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroViajes_viaje_ViajeIdViaje",
                        column: x => x.ViajeIdViaje,
                        principalTable: "viaje",
                        principalColumn: "IdViaje",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroViajes_BusIdBus",
                table: "RegistroViajes",
                column: "BusIdBus");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroViajes_ChoferIdChofer",
                table: "RegistroViajes",
                column: "ChoferIdChofer");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroViajes_TramoIdTramo",
                table: "RegistroViajes",
                column: "TramoIdTramo");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroViajes_ViajeIdViaje",
                table: "RegistroViajes",
                column: "ViajeIdViaje");

            migrationBuilder.CreateIndex(
                name: "IX_viaje_BusId",
                table: "viaje",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_viaje_BusIdBus",
                table: "viaje",
                column: "BusIdBus");

            migrationBuilder.CreateIndex(
                name: "IX_viaje_ChoferId",
                table: "viaje",
                column: "ChoferId");

            migrationBuilder.CreateIndex(
                name: "IX_viaje_ChoferIdChofer",
                table: "viaje",
                column: "ChoferIdChofer");

            migrationBuilder.CreateIndex(
                name: "IX_viaje_TramoId",
                table: "viaje",
                column: "TramoId");

            migrationBuilder.CreateIndex(
                name: "IX_viaje_TramoIdTramo",
                table: "viaje",
                column: "TramoIdTramo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroViajes");

            migrationBuilder.DropTable(
                name: "viaje");

            migrationBuilder.DropTable(
                name: "buses");

            migrationBuilder.DropTable(
                name: "chofer");

            migrationBuilder.DropTable(
                name: "tramo");
        }
    }
}
