using Komunikator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikator
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<MessageModel> Messages { get; set; }

        public DbSet<UserModel> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\USER\source\repos\Komunikator\Komunikator\Database\MessagesHistory.db");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Messages)
                .WithOne();

            modelBuilder.Entity<UserModel>()
                .Navigation(u => u.Messages)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
                
        }


    }
}
