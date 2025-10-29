using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Torneios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TamanhoChave = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TorneioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Torneios_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TorneioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rodada = table.Column<int>(type: "int", nullable: false),
                    ParticipanteAId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParticipanteBId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgendadaPara = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    VencedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Placar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidas_Torneios_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_TorneioId_ParticipanteId",
                table: "Inscricoes",
                columns: new[] { "TorneioId", "ParticipanteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Email",
                table: "Participantes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_TorneioId_Rodada",
                table: "Partidas",
                columns: new[] { "TorneioId", "Rodada" });

            migrationBuilder.CreateIndex(
                name: "IX_Torneios_Nome",
                table: "Torneios",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricoes");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Torneios");
        }
    }
}
