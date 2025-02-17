using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Feedback_App.Migrations
{
    /// <inheritdoc />
    public partial class Fixxtoseedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "588ba132-f018-4b61-bce9-4977098b1146",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "e026c331-dc33-4b33-888e-b750f09e544b", "AQAAAAIAAYagAAAAEB84KXJ2FP+WLIUNE1gS8+jqeo0fQW2z17NKpwY9VlZ2kv+i9hKBHWxtJt60r4oEbQ==", "https://localhost:7185/images/profiles/7a95e6c4-31da-4c14-8760-f5de479f1698.png", "2e6b1ee0-813c-4fed-8fa1-5045805d3e2f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "588ba132-f018-4b61-bce9-4977098b1146",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "1f3abc55-6656-4213-9f8d-05219d5b86fa", "AQAAAAIAAYagAAAAEBT9B6EyAZQ+nL2OOT2Mwb8b7mfR98UFbTIsRo8cIBCssDqDe2nz7qqqo49AxJyUCA==", "7a95e6c4-31da-4c14-8760-f5de479f1698.png", "46c2e9f2-11e0-40eb-a0ce-40733c9ef89a" });
        }
    }
}
