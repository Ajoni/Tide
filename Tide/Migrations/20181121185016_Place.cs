using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tide.Migrations
{
    public partial class Place : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavPlaceId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavPlaceId",
                table: "Users",
                column: "FavPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Place_FavPlaceId",
                table: "Users",
                column: "FavPlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Place_FavPlaceId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavPlaceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavPlaceId",
                table: "Users");
        }
    }
}
