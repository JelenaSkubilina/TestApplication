using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class RemoveTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Configurations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Title",
                table: "Configurations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
