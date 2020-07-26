using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialSiteRepositoryLayer.Migrations
{
    public partial class Friends2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Friends",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Friends");
        }
    }
}
