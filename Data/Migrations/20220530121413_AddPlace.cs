using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meetup.Migrations
{
    public partial class AddPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "EventPlaceId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.PlaceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventPlaceId",
                table: "Events",
                column: "EventPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Place_EventPlaceId",
                table: "Events",
                column: "EventPlaceId",
                principalTable: "Place",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Place_EventPlaceId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventPlaceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventPlaceId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
