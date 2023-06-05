using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Repositoryy.Configurations
{
    internal class ScoreConfiguration : IEntityTypeConfiguration<VideoInterviewScore>
    {
        public void Configure(EntityTypeBuilder<VideoInterviewScore> builder)
        {
            builder.HasKey(x => x.Id);
            //Id identity column olsun, 1er 1er artsın.
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.score_type).IsRequired().HasMaxLength(20);
            builder.Property(x => x.score).IsRequired();

            builder.ToTable("Scores");

            //1 to 1 relationship
            //builder.HasOne(x => x.VideoInterview).WithOne(x => x.Score);
        }
    }
}
