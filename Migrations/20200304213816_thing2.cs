using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopAPI.Migrations
{
  public partial class thing2 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {

      migrationBuilder.AddColumn<int>(
          name: "LocationId",
          table: "HockeySticks",
          nullable: true);


      migrationBuilder.CreateIndex(
          name: "IX_HockeySticks_LocationId",
          table: "HockeySticks",
          column: "LocationId");

      migrationBuilder.AddForeignKey(
          name: "FK_HockeySticks_Locations_LocationId",
          table: "HockeySticks",
          column: "LocationId",
          principalTable: "Locations",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_HockeySticks_Locations_LocationId",
          table: "HockeySticks");

      migrationBuilder.DropPrimaryKey(
          name: "PK_HockeySticks",
          table: "HockeySticks");

      migrationBuilder.DropIndex(
          name: "IX_HockeySticks_LocationId",
          table: "HockeySticks");

      migrationBuilder.DropColumn(
          name: "LocationId",
          table: "HockeySticks");

      migrationBuilder.RenameTable(
          name: "HockeySticks",
          newName: "HockeyStick");

      migrationBuilder.AddPrimaryKey(
          name: "PK_HockeyStick",
          table: "HockeyStick",
          column: "Id");
    }
  }
}
