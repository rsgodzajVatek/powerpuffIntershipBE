using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerPuffBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReactorLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReactorLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactorLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactorLocations_Reactor_ReactorId",
                        column: x => x.ReactorId,
                        principalTable: "Reactor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReactorLocations_ReactorId",
                table: "ReactorLocations",
                column: "ReactorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReactorLocations");
        }
    }
}
