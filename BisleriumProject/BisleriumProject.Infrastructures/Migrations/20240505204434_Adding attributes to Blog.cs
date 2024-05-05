using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisleriumProject.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddingattributestoBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownVoteCount",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVoteCount",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVoteCount",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UpVoteCount",
                table: "Blogs");
        }
    }
}
