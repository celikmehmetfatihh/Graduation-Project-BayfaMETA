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
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasData(
                //new User { Id = 10, userName = "fatihcelik", email = "fatihcelik@metu.edu.tr", password = "123456789aa", name = "Fatih", surname = "Celik", tel_no = "05369945787", role="admin" },
                //new User { Id = 11, userName = "atakaleli", email = "atakaleli@metu.edu.tr", password = "atakaleli2002", name = "Ata", surname = "Kaleli", tel_no = "05338578964", role="applicant"},
                //new User { Id = 12, userName = "dogukanbaysal", email = "dogukanbaysal@metu.edu.tr", password = "baysalbafameta", name = "Dogukan", surname = "Baysal", tel_no = "05489653215", role = "applicant" },
                //new User { Id = 4, userName = "melisacagil", email = "melisacagilgan@metu.edu.tr", password = "melisatimam", name = "Melisa", surname = "Cagilgan", tel_no = "05369941253", role = "applicant" },
                //new User { Id = 5, userName = "mertyigido", email = "mertyigit@metu.edu.tr", password = "merto123", name = "Mert", surname = "Yigit", tel_no = "05362312568", role = "applicant" });
        }
        
    }
}
