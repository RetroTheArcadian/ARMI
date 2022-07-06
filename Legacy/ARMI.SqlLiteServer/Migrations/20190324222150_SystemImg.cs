using Microsoft.EntityFrameworkCore.Migrations;

namespace ARMI.SqlLiteServer.Migrations
{
    public partial class SystemImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Systems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Systems");
        }
    }
}
