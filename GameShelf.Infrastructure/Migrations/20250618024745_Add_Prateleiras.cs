using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Prateleiras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prateleira",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(500)", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prateleira", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prateleira_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantePrateleira",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrateleiraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantePrateleira", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantePrateleira_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantePrateleira_Prateleira_PrateleiraId",
                        column: x => x.PrateleiraId,
                        principalTable: "Prateleira",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantePrateleira_PrateleiraId",
                table: "ParticipantePrateleira",
                column: "PrateleiraId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantePrateleira_UserId",
                table: "ParticipantePrateleira",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prateleira_UserId",
                table: "Prateleira",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantePrateleira");

            migrationBuilder.DropTable(
                name: "Prateleira");
        }
    }
}
