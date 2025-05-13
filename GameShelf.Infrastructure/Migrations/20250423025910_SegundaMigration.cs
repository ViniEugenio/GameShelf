using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Classificacao = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analise_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analise_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnaliseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentario_Analise_AnaliseId",
                        column: x => x.AnaliseId,
                        principalTable: "Analise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analise_JogoId",
                table: "Analise",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Analise_UserId",
                table: "Analise",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_AnaliseId",
                table: "Comentario",
                column: "AnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_UserId",
                table: "Comentario",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Analise");
        }
    }
}
