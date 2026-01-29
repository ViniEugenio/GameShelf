using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixInboxTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inbox_Outbox_EventId",
                table: "Inbox");

            migrationBuilder.DropIndex(
                name: "IX_Inbox_EventId",
                table: "Inbox");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataProcessamentoMensagem",
                table: "Inbox",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataProcessamentoMensagem",
                table: "Inbox",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_EventId",
                table: "Inbox",
                column: "EventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inbox_Outbox_EventId",
                table: "Inbox",
                column: "EventId",
                principalTable: "Outbox",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
