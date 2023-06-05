﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NLayer.Repositoryy;

#nullable disable

namespace NLayer.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230605165859_initial101")]
    partial class initial101
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NLayer.Core.Models.InterviewQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuestionBankId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionBankId");

                    b.ToTable("InterviewQuestions");
                });

            modelBuilder.Entity("NLayer.Core.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FirstStageEndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<float>("ResumeMultiplier")
                        .HasColumnType("real");

                    b.Property<DateTime>("SecondStageEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StageOneThreshold")
                        .HasColumnType("int");

                    b.Property<float>("TechnicalTestMultiplier")
                        .HasColumnType("real");

                    b.Property<float>("VideoInterviewMultiplier")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstStageEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = false,
                            JobDescription = "Backend developer experienced in C#",
                            JobTitle = "Backend Developer",
                            NumberOfPeople = 10,
                            ResumeMultiplier = 0f,
                            SecondStageEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TechnicalTestMultiplier = 0f,
                            VideoInterviewMultiplier = 0f
                        },
                        new
                        {
                            Id = 2,
                            FirstStageEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = false,
                            JobDescription = "Frontend developer experienced in Java, HTML, CSS",
                            JobTitle = "Frontend Developer",
                            NumberOfPeople = 5,
                            ResumeMultiplier = 0f,
                            SecondStageEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TechnicalTestMultiplier = 0f,
                            VideoInterviewMultiplier = 0f
                        });
                });

            modelBuilder.Entity("NLayer.Core.Models.QuestionBank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("QuestionBank");
                });

            modelBuilder.Entity("NLayer.Core.Models.Resume", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("UserId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("NLayer.Core.Models.ResumeConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("EduMultiplier")
                        .HasColumnType("real");

                    b.Property<float?>("ExpMultiplier")
                        .HasColumnType("real");

                    b.Property<string>("JobPositions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("KeywordMultiplier")
                        .HasColumnType("real");

                    b.Property<float?>("MinExperience")
                        .HasColumnType("real");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<string>("RequiredSkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("SoftSkillsMultiplier")
                        .HasColumnType("real");

                    b.Property<float?>("TechSkillsMultiplier")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PositionId")
                        .IsUnique()
                        .HasFilter("[PositionId] IS NOT NULL");

                    b.ToTable("ResumeConfigurations");
                });

            modelBuilder.Entity("NLayer.Core.Models.ResumeScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("MatchingScore")
                        .HasColumnType("float");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<double>("TotalScore")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeScores");
                });

            modelBuilder.Entity("NLayer.Core.Models.StageConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<bool?>("VideoInterviewFlag")
                        .HasColumnType("bit");

                    b.Property<int?>("VideoInterviewPercentage")
                        .HasColumnType("int");

                    b.Property<bool?>("cvFlag")
                        .HasColumnType("bit");

                    b.Property<int?>("cvPercentage")
                        .HasColumnType("int");

                    b.Property<int?>("stageOneThreshold")
                        .HasColumnType("int");

                    b.Property<bool?>("technicalFlag")
                        .HasColumnType("bit");

                    b.Property<int?>("technicalPercentage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId")
                        .IsUnique();

                    b.ToTable("StageConfigurations");
                });

            modelBuilder.Entity("NLayer.Core.Models.TechnicalConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ExcelPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionBankId")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId")
                        .IsUnique()
                        .HasFilter("[PositionId] IS NOT NULL");

                    b.HasIndex("QuestionBankId");

                    b.ToTable("TechnicalConfigurations");
                });

            modelBuilder.Entity("NLayer.Core.Models.TechnicalScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("UserId");

                    b.ToTable("TechnicalScores");
                });

            modelBuilder.Entity("NLayer.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("tel_no")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            email = "fatihcelik@metu.edu.tr",
                            name = "Fatih",
                            password = "123456789aa",
                            role = "admin",
                            surname = "Celik",
                            telno = "05369945787",
                            userName = "fatihcelik"
                        },
                        new
                        {
                            Id = 2,
                            email = "atakaleli@metu.edu.tr",
                            name = "Ata",
                            password = "atakaleli2002",
                            role = "applicant",
                            surname = "Kaleli",
                            telno = "05338578964",
                            userName = "atakaleli"
                        },
                        new
                        {
                            Id = 3,
                            email = "dogukanbaysal@metu.edu.tr",
                            name = "Dogukan",
                            password = "baysalbafameta",
                            role = "applicant",
                            surname = "Baysal",
                            telno = "05489653215",
                            userName = "dogukanbaysal"
                        },
                        new
                        {
                            Id = 4,
                            email = "melisacagilgan@metu.edu.tr",
                            name = "Melisa",
                            password = "melisatimam",
                            role = "applicant",
                            surname = "Cagilgan",
                            telno = "05369941253",
                            userName = "melisacagil"
                        });
                });

            modelBuilder.Entity("NLayer.Core.Models.UserPosition", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<float>("FinalScore")
                        .HasColumnType("real");

                    b.Property<bool?>("isFirstStagePassed")
                        .HasColumnType("bit");

                    b.Property<bool?>("isSecondStageFinished")
                        .HasColumnType("bit");

                    b.Property<bool?>("isTechnicalQuestionCompleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("isVideoInterviewCompleted")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("UserPositions");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PositionId = 1,
                            FinalScore = 0f
                        },
                        new
                        {
                            UserId = 2,
                            PositionId = 1,
                            FinalScore = 0f
                        },
                        new
                        {
                            UserId = 3,
                            PositionId = 2,
                            FinalScore = 0f
                        },
                        new
                        {
                            UserId = 4,
                            PositionId = 2,
                            FinalScore = 0f
                        });
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("format")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("UserId");

                    b.ToTable("VideoInterviews", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PositionId = 1,
                            UserId = 1,
                            date = new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9237),
                            format = "mp4",
                            size = 12
                        },
                        new
                        {
                            Id = 2,
                            PositionId = 1,
                            UserId = 2,
                            date = new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9288),
                            format = "mp3",
                            size = 14
                        },
                        new
                        {
                            Id = 3,
                            PositionId = 2,
                            UserId = 3,
                            date = new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9291),
                            format = "mp3",
                            size = 17
                        },
                        new
                        {
                            Id = 4,
                            PositionId = 2,
                            UserId = 4,
                            date = new DateTime(2023, 6, 5, 19, 58, 58, 686, DateTimeKind.Local).AddTicks(9292),
                            format = "mp4",
                            size = 21
                        });
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterviewConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("AgreeablenessMultiplier")
                        .HasColumnType("real");

                    b.Property<int?>("AllocatedTime")
                        .HasColumnType("int");

                    b.Property<float?>("ConscientiousnessMultiplier")
                        .HasColumnType("real");

                    b.Property<float?>("ExtraversionMultiplier")
                        .HasColumnType("real");

                    b.Property<string>("InterviewTopics")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("NeuroticismMultiplier")
                        .HasColumnType("real");

                    b.Property<float?>("OpennessMultiplier")
                        .HasColumnType("real");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId")
                        .IsUnique()
                        .HasFilter("[PositionId] IS NOT NULL");

                    b.ToTable("VideoInterviewConfigurations");
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterviewScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("VideoInterviewId")
                        .HasColumnType("int");

                    b.Property<float>("agreeableness")
                        .HasColumnType("real");

                    b.Property<float>("conscientiousness")
                        .HasColumnType("real");

                    b.Property<string>("details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("extraversion")
                        .HasColumnType("real");

                    b.Property<float>("neuroticism")
                        .HasColumnType("real");

                    b.Property<float>("openness")
                        .HasColumnType("real");

                    b.Property<float>("score")
                        .HasColumnType("real");

                    b.Property<string>("score_type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("VideoInterviewId")
                        .IsUnique();

                    b.ToTable("Scores", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            VideoInterviewId = 1,
                            agreeableness = 0.25f,
                            conscientiousness = 0.85f,
                            details = "some details",
                            extraversion = 0.65f,
                            neuroticism = 0.55f,
                            openness = 0.47f,
                            score = 0.98f,
                            scoretype = "video interview"
                        },
                        new
                        {
                            Id = 2,
                            VideoInterviewId = 2,
                            agreeableness = 0.33f,
                            conscientiousness = 0.86f,
                            details = "some details",
                            extraversion = 0.123f,
                            neuroticism = 0.861f,
                            openness = 0.923f,
                            score = 0.2f,
                            scoretype = "video interview"
                        },
                        new
                        {
                            Id = 3,
                            VideoInterviewId = 3,
                            agreeableness = 0.83f,
                            conscientiousness = 0.24f,
                            details = "some details",
                            extraversion = 0.32f,
                            neuroticism = 0.635f,
                            openness = 0.47f,
                            score = 0.84f,
                            scoretype = "video interview"
                        },
                        new
                        {
                            Id = 4,
                            VideoInterviewId = 4,
                            agreeableness = 0.64f,
                            conscientiousness = 0.85f,
                            details = "some details",
                            extraversion = 0.78f,
                            neuroticism = 0.55f,
                            openness = 0.47f,
                            score = 0.87f,
                            scoretype = "video interview"
                        });
                });

            modelBuilder.Entity("NLayer.Core.Models.InterviewQuestion", b =>
                {
                    b.HasOne("NLayer.Core.Models.QuestionBank", "QuestionBank")
                        .WithMany("InterviewQuestions")
                        .HasForeignKey("QuestionBankId");

                    b.Navigation("QuestionBank");
                });

            modelBuilder.Entity("NLayer.Core.Models.Resume", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.ResumeConfiguration", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithOne("ResumeConfiguration")
                        .HasForeignKey("NLayer.Core.Models.ResumeConfiguration", "PositionId");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("NLayer.Core.Models.ResumeScore", b =>
                {
                    b.HasOne("NLayer.Core.Models.Resume", "Resume")
                        .WithMany()
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("NLayer.Core.Models.StageConfiguration", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithOne("StageConfiguration")
                        .HasForeignKey("NLayer.Core.Models.StageConfiguration", "PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("NLayer.Core.Models.TechnicalConfiguration", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithOne("TechnicalConfiguration")
                        .HasForeignKey("NLayer.Core.Models.TechnicalConfiguration", "PositionId");

                    b.HasOne("NLayer.Core.Models.QuestionBank", "QuestionBank")
                        .WithMany()
                        .HasForeignKey("QuestionBankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("QuestionBank");
                });

            modelBuilder.Entity("NLayer.Core.Models.TechnicalScore", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.UserPosition", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithMany("userPositions")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany("userPositions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterview", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithMany("videoInterviews")
                        .HasForeignKey("PositionId");

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany("videoInterviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterviewConfiguration", b =>
                {
                    b.HasOne("NLayer.Core.Models.Position", "Position")
                        .WithOne("VideoInterviewConfiguration")
                        .HasForeignKey("NLayer.Core.Models.VideoInterviewConfiguration", "PositionId");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterviewScore", b =>
                {
                    b.HasOne("NLayer.Core.Models.VideoInterview", "VideoInterview")
                        .WithOne("VideoInterviewScore")
                        .HasForeignKey("NLayer.Core.Models.VideoInterviewScore", "VideoInterviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VideoInterview");
                });

            modelBuilder.Entity("NLayer.Core.Models.Position", b =>
                {
                    b.Navigation("ResumeConfiguration");

                    b.Navigation("StageConfiguration");

                    b.Navigation("TechnicalConfiguration");

                    b.Navigation("VideoInterviewConfiguration");

                    b.Navigation("userPositions");

                    b.Navigation("videoInterviews");
                });

            modelBuilder.Entity("NLayer.Core.Models.QuestionBank", b =>
                {
                    b.Navigation("InterviewQuestions");
                });

            modelBuilder.Entity("NLayer.Core.Models.User", b =>
                {
                    b.Navigation("userPositions");

                    b.Navigation("videoInterviews");
                });

            modelBuilder.Entity("NLayer.Core.Models.VideoInterview", b =>
                {
                    b.Navigation("VideoInterviewScore")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
