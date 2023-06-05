﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Positions",
                newName: "SecondStageEndDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Positions",
                newName: "FirstStageEndDate");

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
                value: new DateTime(2023, 5, 10, 20, 3, 47, 2, DateTimeKind.Local).AddTicks(1559));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2023, 5, 10, 20, 3, 47, 2, DateTimeKind.Local).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "date",
                value: new DateTime(2023, 5, 10, 20, 3, 47, 2, DateTimeKind.Local).AddTicks(1598));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "date",
                value: new DateTime(2023, 5, 10, 20, 3, 47, 2, DateTimeKind.Local).AddTicks(1600));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondStageEndDate",
                table: "Positions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "FirstStageEndDate",
                table: "Positions",
                newName: "EndDate");

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
                value: new DateTime(2023, 5, 9, 19, 35, 21, 331, DateTimeKind.Local).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2023, 5, 9, 19, 35, 21, 331, DateTimeKind.Local).AddTicks(5032));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "date",
                value: new DateTime(2023, 5, 9, 19, 35, 21, 331, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "date",
                value: new DateTime(2023, 5, 9, 19, 35, 21, 331, DateTimeKind.Local).AddTicks(5036));
        }
    }
}
