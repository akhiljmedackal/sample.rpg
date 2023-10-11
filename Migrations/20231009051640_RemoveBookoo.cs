using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sample.rpg.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBookoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateofPublish",
                table: "BookDemo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofPublish",
                table: "BookDemo");
        }
    }
}
