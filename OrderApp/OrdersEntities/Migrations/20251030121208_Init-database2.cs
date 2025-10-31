using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersEntities.Migrations
{
    /// <inheritdoc />
    public partial class Initdatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2025, 10, 30, 15, 9, 28, 306, DateTimeKind.Local).AddTicks(2957));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2025, 10, 30, 15, 9, 28, 300, DateTimeKind.Local).AddTicks(3226));
        }
    }
}
