using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payam.Migrations
{
    /// <inheritdoc />
    public partial class AddNoDeviceIdToDeviceAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DeviceActions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "DeviceActions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
