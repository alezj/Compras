using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compras.Migrations
{
    public partial class usu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_States_countries_CountryID",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_Name_CountryID",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_StateID",
                table: "Cities");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "States",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Cities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name_CountryID",
                table: "States",
                columns: new[] { "Name", "CountryID" },
                unique: true,
                filter: "[CountryID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_StateID",
                table: "Cities",
                columns: new[] { "Name", "StateID" },
                unique: true,
                filter: "[StateID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityID",
                table: "AspNetUsers",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StateID",
                table: "Cities",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_States_countries_CountryID",
                table: "States",
                column: "CountryID",
                principalTable: "countries",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_States_countries_CountryID",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_Name_CountryID",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_StateID",
                table: "Cities");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "States",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_Name_CountryID",
                table: "States",
                columns: new[] { "Name", "CountryID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_StateID",
                table: "Cities",
                columns: new[] { "Name", "StateID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityID",
                table: "AspNetUsers",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StateID",
                table: "Cities",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_States_countries_CountryID",
                table: "States",
                column: "CountryID",
                principalTable: "countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
