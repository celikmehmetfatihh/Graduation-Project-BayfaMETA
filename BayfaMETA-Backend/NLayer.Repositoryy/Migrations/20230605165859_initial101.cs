using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 1,
                column: "score_type",
                value: "video interview");

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 2,
                column: "score_type",
                value: "video interview");

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 3,
                column: "score_type",
                value: "video interview");

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 4,
                column: "score_type",
                value: "video interview");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "tel_no",
                value: "05369945787");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "tel_no",
                value: "05338578964");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "tel_no",
                value: "05489653215");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "tel_no",
                value: "05369941253");

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "date",
                value: new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9237));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9288));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "date",
                value: new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9291));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "date",
                value: new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9292));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 1,
                column: "score_type",
                value: null);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 2,
                column: "score_type",
                value: null);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 3,
                column: "score_type",
                value: null);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 4,
                column: "score_type",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "tel_no",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "tel_no",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "tel_no",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "tel_no",
                value: null);

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "date",
                value: new DateTime(2023, 5, 27, 12, 42, 9, 928, DateTimeKind.Local).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2023, 5, 27, 12, 42, 9, 928, DateTimeKind.Local).AddTicks(7473));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "date",
                value: new DateTime(2023, 5, 27, 12, 42, 9, 928, DateTimeKind.Local).AddTicks(7475));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "date",
                value: new DateTime(2023, 5, 27, 12, 42, 9, 928, DateTimeKind.Local).AddTicks(7477));
        }
    }
}
