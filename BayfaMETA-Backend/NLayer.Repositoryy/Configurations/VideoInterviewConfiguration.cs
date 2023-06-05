using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repositoryy.Configurations
{
    internal class VideoInterviewConfiguration : IEntityTypeConfiguration<VideoInterview>
    {
        public void Configure(EntityTypeBuilder<VideoInterview> builder)
        {
            builder.HasKey(x => x.Id);
            //Id identity column olsun, 1er 1er artsın.
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.format).IsRequired().HasMaxLength(50);

            builder.ToTable("VideoInterviews");
            //video interview can have one user, user can have multiple video interviews
            builder.HasOne(x => x.User).WithMany(x => x.videoInterviews).HasForeignKey(x => x.UserId);
        }

    }
}
