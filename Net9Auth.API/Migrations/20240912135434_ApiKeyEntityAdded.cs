using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net9Auth.API.Migrations
{
    /// <inheritdoc />
    public partial class ApiKeyEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RevokedReason",
                table: "ApiKeys",
                newName: "RevokeReason");

            migrationBuilder.RenameColumn(
                name: "Revoked",
                table: "ApiKeys",
                newName: "IsRevoked");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RevokedAt",
                table: "ApiKeys",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "ApiKeys",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ApiKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                table: "ApiKeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ApiKeys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ApiKeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ApiKeys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteReason",
                table: "ApiKeys");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ApiKeys");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ApiKeys");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ApiKeys");

            migrationBuilder.RenameColumn(
                name: "RevokeReason",
                table: "ApiKeys",
                newName: "RevokedReason");

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "ApiKeys",
                newName: "Revoked");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RevokedAt",
                table: "ApiKeys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "ApiKeys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ApiKeys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
