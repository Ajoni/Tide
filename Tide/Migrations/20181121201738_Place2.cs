using Microsoft.EntityFrameworkCore.Migrations;

namespace Tide.Migrations
{
    public partial class Place2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Place_FavPlaceId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Places_FavPlaceId",
                table: "Users",
                column: "FavPlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Places_FavPlaceId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Place_FavPlaceId",
                table: "Users",
                column: "FavPlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
