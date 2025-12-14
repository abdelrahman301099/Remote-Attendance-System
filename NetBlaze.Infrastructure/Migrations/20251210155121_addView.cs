using Microsoft.EntityFrameworkCore.Migrations;
using NetBlaze.Infrastructure.Views;

#nullable disable

namespace NetBlaze.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(VwUserPoliciesReportMg.Up());

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(VwUserPoliciesReportMg.Down());

        }
    }
}
