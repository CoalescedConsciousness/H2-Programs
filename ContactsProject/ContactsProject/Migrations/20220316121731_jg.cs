using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsProject.Migrations
{
    public partial class jg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Contact",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Contact");
        }
    }
}
