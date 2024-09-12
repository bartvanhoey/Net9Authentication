using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net9Auth.API.Migrations
{
    /// <inheritdoc />
    public partial class ExceptionLogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExceptionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityLevel = table.Column<int>(type: "int", nullable: true),
                    ResolutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionFrequency = table.Column<long>(type: "bigint", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExceptionLogs");
        }
    }
}
