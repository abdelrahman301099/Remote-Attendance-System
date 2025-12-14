using Microsoft.EntityFrameworkCore.Migrations;
using NetBlaze.Infrastructure.Views;

#nullable disable

namespace NetBlaze.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(VwAttendanceFlatMg.Up());
            migrationBuilder.Sql(VwRandomCheckFlatMg.Up());
         

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(VwAttendanceFlatMg.Down());
            migrationBuilder.Sql(VwRandomCheckFlatMg.Down());
           

        }
    }
}
