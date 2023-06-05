using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class scoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FinalScore",
                table: "UserPositions",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "isFirstStagePassed",
                table: "UserPositions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSecondStageFinished",
                table: "UserPositions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Positions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StageOneThreshold",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsClosed", "StageOneThreshold" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsClosed", "StageOneThreshold" },
                values: new object[] { null, null });

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
                table: "UserPositions",
                keyColumns: new[] { "PositionId", "UserId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "FinalScore", "isFirstStagePassed", "isSecondStageFinished" },
                values: new object[] { 0f, null, null });

            migrationBuilder.UpdateData(
                table: "UserPositions",
                keyColumns: new[] { "PositionId", "UserId" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "FinalScore", "isFirstStagePassed", "isSecondStageFinished" },
                values: new object[] { 0f, null, null });

            migrationBuilder.UpdateData(
                table: "UserPositions",
                keyColumns: new[] { "PositionId", "UserId" },
                keyValues: new object[] { 2, 3 },
                columns: new[] { "FinalScore", "isFirstStagePassed", "isSecondStageFinished" },
                values: new object[] { 0f, null, null });

            migrationBuilder.UpdateData(
                table: "UserPositions",
                keyColumns: new[] { "PositionId", "UserId" },
                keyValues: new object[] { 2, 4 },
                columns: new[] { "FinalScore", "isFirstStagePassed", "isSecondStageFinished" },
                values: new object[] { 0f, null, null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalScore",
                table: "UserPositions");

            migrationBuilder.DropColumn(
                name: "isFirstStagePassed",
                table: "UserPositions");

            migrationBuilder.DropColumn(
                name: "isSecondStageFinished",
                table: "UserPositions");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "StageOneThreshold",
                table: "Positions");

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
                value: new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "date",
                value: new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "VideoInterviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "date",
                value: new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1202));
        }
    }
}
