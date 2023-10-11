using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sample.rpg.Migrations
{
    /// <inheritdoc />
    public partial class Book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSaltk",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "BookDemo",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofPublish = table.Column<int>(type: "int", nullable: false),
                    PriceofBook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDemo", x => x.BookId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookDemo");

            migrationBuilder.DropColumn(
                name: "PasswordSaltk",
                table: "Users");
        }
    }
}
