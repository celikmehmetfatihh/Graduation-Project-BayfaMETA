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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            //Id identity column olsun, 1er 1er artsın.
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.password).IsRequired().HasMaxLength(50);
            builder.Property(x => x.name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.surname).IsRequired().HasMaxLength(50);

            builder.ToTable("Users");
        }
    }
}
