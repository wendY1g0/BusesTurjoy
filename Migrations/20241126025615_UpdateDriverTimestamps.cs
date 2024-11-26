using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personalMantencion.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDriverTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "drivers",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "IsAvailable",
                table: "drivers",
                type: "varchar(3)",
                nullable: false,
                defaultValueSql: "'YES'",
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldDefaultValue: "YES");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "drivers",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "drivers",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<string>(
                name: "IsAvailable",
                table: "drivers",
                type: "varchar(3)",
                nullable: false,
                defaultValue: "YES",
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldDefaultValueSql: "'YES'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "drivers",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
