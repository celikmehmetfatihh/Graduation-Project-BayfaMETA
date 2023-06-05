using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NLayer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    TechnicalTestMultiplier = table.Column<float>(type: "real", nullable: false),
                    VideoInterviewMultiplier = table.Column<float>(type: "real", nullable: false),
                    ResumeMultiplier = table.Column<float>(type: "real", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telno = table.Column<string>(name: "tel_no", type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResumeConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    JobPositions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinExperience = table.Column<float>(type: "real", nullable: true),
                    EduMultiplier = table.Column<float>(type: "real", nullable: true),
                    ExpMultiplier = table.Column<float>(type: "real", nullable: true),
                    TechSkillsMultiplier = table.Column<float>(type: "real", nullable: true),
                    SoftSkillsMultiplier = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeConfigurations_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StageConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stageOneThreshold = table.Column<int>(type: "int", nullable: true),
                    VideoInterviewPercentage = table.Column<int>(type: "int", nullable: true),
                    technicalPercentage = table.Column<int>(type: "int", nullable: true),
                    cvPercentage = table.Column<int>(type: "int", nullable: true),
                    VideoInterviewFlag = table.Column<bool>(type: "bit", nullable: true),
                    technicalFlag = table.Column<bool>(type: "bit", nullable: true),
                    cvFlag = table.Column<bool>(type: "bit", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageConfigurations_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoInterviewConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpennessMultiplier = table.Column<float>(type: "real", nullable: true),
                    ConscientiousnessMultiplier = table.Column<float>(type: "real", nullable: true),
                    ExtraversionMultiplier = table.Column<float>(type: "real", nullable: true),
                    AgreeablenessMultiplier = table.Column<float>(type: "real", nullable: true),
                    NeuroticismMultiplier = table.Column<float>(type: "real", nullable: true),
                    InterviewTopics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocatedTime = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInterviewConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoInterviewConfigurations_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterviewQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionBankId = table.Column<int>(type: "int", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewQuestions_QuestionBank_QuestionBankId",
                        column: x => x.QuestionBankId,
                        principalTable: "QuestionBank",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TechnicalConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    QuestionBankId = table.Column<int>(type: "int", nullable: false),
                    ExcelPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalConfigurations_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TechnicalConfigurations_QuestionBank_QuestionBankId",
                        column: x => x.QuestionBankId,
                        principalTable: "QuestionBank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resumes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalScores_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicalScores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPositions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    isTechnicalQuestionCompleted = table.Column<bool>(type: "bit", nullable: true),
                    isVideoInterviewCompleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPositions", x => new { x.UserId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_UserPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPositions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoInterviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    size = table.Column<int>(type: "int", nullable: true),
                    format = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInterviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoInterviews_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoInterviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchingScore = table.Column<double>(type: "float", nullable: false),
                    TotalScore = table.Column<double>(type: "float", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeScores_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scoretype = table.Column<string>(name: "score_type", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    score = table.Column<float>(type: "real", nullable: false),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extraversion = table.Column<float>(type: "real", nullable: false),
                    agreeableness = table.Column<float>(type: "real", nullable: false),
                    openness = table.Column<float>(type: "real", nullable: false),
                    conscientiousness = table.Column<float>(type: "real", nullable: false),
                    neuroticism = table.Column<float>(type: "real", nullable: false),
                    VideoInterviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_VideoInterviews_VideoInterviewId",
                        column: x => x.VideoInterviewId,
                        principalTable: "VideoInterviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "EndDate", "IsAvailable", "JobDescription", "JobTitle", "NumberOfPeople", "ResumeMultiplier", "StartDate", "TechnicalTestMultiplier", "VideoInterviewMultiplier" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Backend developer experienced in C#", "Backend Developer", 10, 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0f, 0f },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Frontend developer experienced in Java, HTML, CSS", "Frontend Developer", 5, 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0f, 0f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "email", "name", "password", "role", "surname", "tel_no", "userName" },
                values: new object[,]
                {
                    { 1, "fatihcelik@metu.edu.tr", "Fatih", "123456789aa", "admin", "Celik", "05369945787", "fatihcelik" },
                    { 2, "atakaleli@metu.edu.tr", "Ata", "atakaleli2002", "applicant", "Kaleli", "05338578964", "atakaleli" },
                    { 3, "dogukanbaysal@metu.edu.tr", "Dogukan", "baysalbafameta", "applicant", "Baysal", "05489653215", "dogukanbaysal" },
                    { 4, "melisacagilgan@metu.edu.tr", "Melisa", "melisatimam", "applicant", "Cagilgan", "05369941253", "melisacagil" }
                });

            migrationBuilder.InsertData(
                table: "UserPositions",
                columns: new[] { "PositionId", "UserId", "isTechnicalQuestionCompleted", "isVideoInterviewCompleted" },
                values: new object[,]
                {
                    { 1, 1, null, null },
                    { 1, 2, null, null },
                    { 2, 3, null, null },
                    { 2, 4, null, null }
                });

            migrationBuilder.InsertData(
                table: "VideoInterviews",
                columns: new[] { "Id", "PositionId", "UserId", "date", "format", "path", "size" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1149), "mp4", null, 12 },
                    { 2, 1, 2, new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1198), "mp3", null, 14 },
                    { 3, 2, 3, new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1200), "mp3", null, 17 },
                    { 4, 2, 4, new DateTime(2023, 5, 5, 22, 6, 31, 790, DateTimeKind.Local).AddTicks(1202), "mp4", null, 21 }
                });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "VideoInterviewId", "agreeableness", "conscientiousness", "details", "extraversion", "neuroticism", "openness", "score", "score_type" },
                values: new object[,]
                {
                    { 1, 1, 0.25f, 0.85f, "some details", 0.65f, 0.55f, 0.47f, 0.98f, "video interview" },
                    { 2, 2, 0.33f, 0.86f, "some details", 0.123f, 0.861f, 0.923f, 0.2f, "video interview" },
                    { 3, 3, 0.83f, 0.24f, "some details", 0.32f, 0.635f, 0.47f, 0.84f, "video interview" },
                    { 4, 4, 0.64f, 0.85f, "some details", 0.78f, 0.55f, 0.47f, 0.87f, "video interview" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewQuestions_QuestionBankId",
                table: "InterviewQuestions",
                column: "QuestionBankId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeConfigurations_PositionId",
                table: "ResumeConfigurations",
                column: "PositionId",
                unique: true,
                filter: "[PositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_PositionId",
                table: "Resumes",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeScores_ResumeId",
                table: "ResumeScores",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_VideoInterviewId",
                table: "Scores",
                column: "VideoInterviewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageConfigurations_PositionId",
                table: "StageConfigurations",
                column: "PositionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalConfigurations_PositionId",
                table: "TechnicalConfigurations",
                column: "PositionId",
                unique: true,
                filter: "[PositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalConfigurations_QuestionBankId",
                table: "TechnicalConfigurations",
                column: "QuestionBankId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalScores_PositionId",
                table: "TechnicalScores",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalScores_UserId",
                table: "TechnicalScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPositions_PositionId",
                table: "UserPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoInterviewConfigurations_PositionId",
                table: "VideoInterviewConfigurations",
                column: "PositionId",
                unique: true,
                filter: "[PositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VideoInterviews_PositionId",
                table: "VideoInterviews",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoInterviews_UserId",
                table: "VideoInterviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewQuestions");

            migrationBuilder.DropTable(
                name: "ResumeConfigurations");

            migrationBuilder.DropTable(
                name: "ResumeScores");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "StageConfigurations");

            migrationBuilder.DropTable(
                name: "TechnicalConfigurations");

            migrationBuilder.DropTable(
                name: "TechnicalScores");

            migrationBuilder.DropTable(
                name: "UserPositions");

            migrationBuilder.DropTable(
                name: "VideoInterviewConfigurations");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "VideoInterviews");

            migrationBuilder.DropTable(
                name: "QuestionBank");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
