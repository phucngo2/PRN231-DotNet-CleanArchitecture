using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditLog_AddColumn_Path : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "AuditLog",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "AuditLog");
        }
    }
}
