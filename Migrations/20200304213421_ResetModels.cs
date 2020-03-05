using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ShopAPI.Migrations
{
  public partial class ResetModels : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Locations",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Address = table.Column<string>(nullable: true),
            ManagerName = table.Column<string>(nullable: true),
            PhoneNumber = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Locations", x => x.Id);
          });

      // migrationBuilder.CreateTable(
      //     name: "HockeySticks",
      //     columns: table => new
      //     {
      //         Id = table.Column<int>(nullable: false)
      //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
      //         SKU = table.Column<int>(nullable: false),
      //         Name = table.Column<string>(nullable: true),
      //         Description = table.Column<string>(nullable: true),
      //         NumberInStock = table.Column<int>(nullable: false),
      //         Price = table.Column<decimal>(nullable: false),
      //         DateOrdered = table.Column<DateTime>(nullable: false),
      //         LocationId = table.Column<int>(nullable: true)
      //     },
      //     constraints: table =>
      //     {
      //         table.PrimaryKey("PK_HockeySticks", x => x.Id);
      //         table.ForeignKey(
      //             name: "FK_HockeySticks_Locations_LocationId",
      //             column: x => x.LocationId,
      //             principalTable: "Locations",
      //             principalColumn: "Id",
      //             onDelete: ReferentialAction.Restrict);
      //     });

      // migrationBuilder.CreateIndex(
      //     name: "IX_HockeySticks_LocationId",
      //     table: "HockeySticks",
      //     column: "LocationId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "HockeySticks");

      migrationBuilder.DropTable(
          name: "Locations");
    }
  }
}
