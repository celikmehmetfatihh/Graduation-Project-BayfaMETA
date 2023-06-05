using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repositoryy.Seeds
{
    internal class VideoInterviewSeed : IEntityTypeConfiguration<VideoInterview>
    {
        public void Configure(EntityTypeBuilder<VideoInterview> builder)
        {
            //builder.HasData(
            //  new VideoInterview { Id = 1, date = DateTime.Now, size = 12, format = "mp4", UserId=1},
            //  new VideoInterview { Id = 2, date = DateTime.Now, size = 14, format = "mp3", UserId=2 },
            //  new VideoInterview { Id = 3, date = DateTime.Now, size = 17, format = "mp3", UserId=3 },
            //  new VideoInterview { Id = 4, date = DateTime.Now, size = 21, format = "mp4", UserId=4 },
            //  new VideoInterview { Id = 5, date = DateTime.Now, size = 23, format = "mp4", UserId=5 });
        }
    }
}
