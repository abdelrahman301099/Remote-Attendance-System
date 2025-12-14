using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBlaze.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixSchmema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RandomlyChecks_Users_UserId1",
                table: "RandomlyChecks");

            migrationBuilder.DropIndex(
                name: "IX_RandomlyChecks_UserId1",
                table: "RandomlyChecks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RandomlyChecks");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "RandomlyChecks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RandomlyChecks_UserId",
                table: "RandomlyChecks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RandomlyChecks_Users_UserId",
                table: "RandomlyChecks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RandomlyChecks_Users_UserId",
                table: "RandomlyChecks");

            migrationBuilder.DropIndex(
                name: "IX_RandomlyChecks_UserId",
                table: "RandomlyChecks");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RandomlyChecks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "RandomlyChecks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RandomlyChecks_UserId1",
                table: "RandomlyChecks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RandomlyChecks_Users_UserId1",
                table: "RandomlyChecks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
