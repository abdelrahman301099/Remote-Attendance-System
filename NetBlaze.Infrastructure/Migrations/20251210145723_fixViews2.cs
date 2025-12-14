using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBlaze.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixViews2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_CompanyPolicies_CompanyPolicyId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_CompanyPolicyId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "WorkEndTime",
                table: "CompanyPolicies");

            migrationBuilder.DropColumn(
                name: "CompanyPolicyId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Attendances",
                newName: "DayDate");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "WorkStartTime",
                table: "CompanyPolicies",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "MaxLate",
                table: "CompanyPolicies",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Attendances",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<double>(
                name: "WorkedHourse",
                table: "Attendances",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "AppliedPolicies",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CompanyPolicyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedPolicies", x => new { x.UserId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_AppliedPolicies_CompanyPolicies_CompanyPolicyId",
                        column: x => x.CompanyPolicyId,
                        principalTable: "CompanyPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedPolicies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedPolicies_CompanyPolicyId",
                table: "AppliedPolicies",
                column: "CompanyPolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedPolicies");

            migrationBuilder.DropColumn(
                name: "MaxLate",
                table: "CompanyPolicies");

            migrationBuilder.DropColumn(
                name: "WorkedHourse",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "DayDate",
                table: "Attendances",
                newName: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkStartTime",
                table: "CompanyPolicies",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkEndTime",
                table: "CompanyPolicies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Attendances",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AddColumn<int>(
                name: "CompanyPolicyId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CompanyPolicyId",
                table: "Attendances",
                column: "CompanyPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_CompanyPolicies_CompanyPolicyId",
                table: "Attendances",
                column: "CompanyPolicyId",
                principalTable: "CompanyPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
