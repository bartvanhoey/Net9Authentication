using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net9Auth.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueToApiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiKeyAuthorizationFilters",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_Key",
                table: "ApiKeyAuthorizationFilters",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApiKeys_Key",
                table: "ApiKeyAuthorizationFilters");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiKeyAuthorizationFilters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
