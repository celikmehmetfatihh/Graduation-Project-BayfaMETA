using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;


namespace NLayer.Repositoryy
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }


        //Creating Tables for our models in the database
        public DbSet<User> Users { get; set; }
        public DbSet<VideoInterview> VideoInterviews { get; set; }
        public DbSet<VideoInterviewScore> VideoInterviewScores { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<StageConfiguration> StageConfigurations { get; set; }
        public DbSet<TechnicalConfiguration> TechnicalConfigurations { get; set; }
        public DbSet<VideoInterviewConfiguration> VideoInterviewConfigurations { get; set; }
        public DbSet<UserPosition> UserPositions { get; set; }
        public DbSet<ResumeConfiguration> ResumeConfigurations { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ResumeScore> ResumeScores { get; set; }
        public DbSet<InterviewQuestion> InterviewQuestions { get; set; }
        public DbSet<QuestionBank> QuestionBank { get; set; }
        public DbSet<TechnicalScore> TechnicalScores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //Adding some mock data to the database so we do not need
            //to add manually each time we alter the database.
            modelBuilder.Entity<Position>().HasData(new Position
            {
                Id = 1,
                JobTitle = "Backend Developer",
                JobDescription = "Backend developer experienced in C#",
                NumberOfPeople = 10
            },
            new Position
            {
                Id = 2,
                JobTitle = "Frontend Developer",
                JobDescription = "Frontend developer experienced in Java, HTML, CSS",
                NumberOfPeople = 5
            });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, userName = "fatihcelik", email = "fatihcelik@metu.edu.tr", password = "123456789aa", name = "Fatih", surname = "Celik", tel_no = "05369945787", role = "admin" },
                new User { Id = 2, userName = "atakaleli", email = "atakaleli@metu.edu.tr", password = "atakaleli2002", name = "Ata", surname = "Kaleli", tel_no = "05338578964", role = "applicant" },
                new User { Id = 3, userName = "dogukanbaysal", email = "dogukanbaysal@metu.edu.tr", password = "baysalbafameta", name = "Dogukan", surname = "Baysal", tel_no = "05489653215", role = "applicant" },
                new User { Id = 4, userName = "melisacagil", email = "melisacagilgan@metu.edu.tr", password = "melisatimam", name = "Melisa", surname = "Cagilgan", tel_no = "05369941253", role = "applicant" });

            modelBuilder.Entity<UserPosition>().HasData(
                new UserPosition { PositionId = 1, UserId = 1 },
                new UserPosition { PositionId = 1, UserId = 2 },
                new UserPosition { PositionId = 2, UserId = 3 },
                new UserPosition { PositionId = 2, UserId = 4 });


            modelBuilder.Entity<VideoInterview>().HasData(
                  new VideoInterview { Id = 1, date = DateTime.Now, size = 12, format = "mp4", UserId = 1, PositionId=1 },
                  new VideoInterview { Id = 2, date = DateTime.Now, size = 14, format = "mp3", UserId = 2, PositionId=1 },
                  new VideoInterview { Id = 3, date = DateTime.Now, size = 17, format = "mp3", UserId = 3, PositionId=2 },
                  new VideoInterview { Id = 4, date = DateTime.Now, size = 21, format = "mp4", UserId = 4, PositionId=2 });

            modelBuilder.Entity<VideoInterviewScore>().HasData(
                new VideoInterviewScore
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
                    score_type = "video interview"
                },
                new VideoInterviewScore
                {
                    Id = 2,
                    VideoInterviewId = 2,
                    agreeableness = 0.33f,
                    conscientiousness = 0.86f,
                    details = "some details",
                    extraversion = 0.123f,
                    neuroticism = 0.861f,
                    openness = 0.923f,
                    score = 0.20f,
                    score_type = "video interview"
                },
                new VideoInterviewScore
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
                    score_type = "video interview"
                },
                new VideoInterviewScore
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
                    score_type = "video interview"
                });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            //Enabling many to many relationship and mapping it to a new table.
            modelBuilder.Entity<UserPosition>()
           .HasKey(t => new { t.UserId, t.PositionId });

            modelBuilder.Entity<UserPosition>()
           .HasOne(pt => pt.User)
           .WithMany(p => p.userPositions)
           .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserPosition>()
                .HasOne(pt => pt.Position)
                .WithMany(t => t.userPositions)
                .HasForeignKey(pt => pt.PositionId);

            base.OnModelCreating(modelBuilder);
        }



    }
}
