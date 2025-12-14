using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBlaze.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixSchema3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedPolicies_Users_UserId",
                table: "AppliedPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_UserId1",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_UserId1",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppliedPolicies",
                table: "AppliedPolicies");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppliedPolicies");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Attendances",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceId",
                table: "AppliedPolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Action",
                table: "AppliedPolicies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PolicyName",
                table: "AppliedPolicies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AppliedPolicies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppliedPolicies",
                table: "AppliedPolicies",
                columns: new[] { "AttendanceId", "PolicyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedPolicies_Attendances_AttendanceId",
                table: "AppliedPolicies",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_UserId",
                table: "Attendances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedPolicies_Attendances_AttendanceId",
                table: "AppliedPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_UserId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppliedPolicies",
                table: "AppliedPolicies");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "AppliedPolicies");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "AppliedPolicies");

            migrationBuilder.DropColumn(
                name: "PolicyName",
                table: "AppliedPolicies");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AppliedPolicies");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Attendances",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Attendances",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AppliedPolicies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppliedPolicies",
                table: "AppliedPolicies",
                columns: new[] { "UserId", "PolicyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId1",
                table: "Attendances",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedPolicies_Users_UserId",
                table: "AppliedPolicies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_UserId1",
                table: "Attendances",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
