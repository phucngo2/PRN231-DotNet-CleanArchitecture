using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditLog_RemovePK_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLog_Users_UserId",
                table: "AuditLog");

            migrationBuilder.DropIndex(
                name: "IX_AuditLog_UserId",
                table: "AuditLog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId",
                table: "AuditLog",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Users_UserId",
                table: "AuditLog",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
