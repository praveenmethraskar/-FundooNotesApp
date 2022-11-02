using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{

    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> UserTable { get; set; }

        public DbSet<NotesEntity> NotesTable { get; set; }

        public DbSet<CollabratorEntity> CollabratorTable { get; set; }

        public DbSet<LabelEntity> LabelTable { get; set; }
    }

}
