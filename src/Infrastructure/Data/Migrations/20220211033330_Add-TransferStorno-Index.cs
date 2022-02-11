using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestJob01.Infrastructure.Data.Migrations
{
    public partial class AddTransferStornoIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transfers_OriginalTransferId",
                table: "Transfers",
                column: "OriginalTransferId",
                unique: true,
                filter: "[OriginalTransferId] is not null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfers_OriginalTransferId",
                table: "Transfers");
        }
    }
}
