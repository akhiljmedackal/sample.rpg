using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sample.rpg.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBooko : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofPublish",
                table: "BookDemo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateofPublish",
                table: "BookDemo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
