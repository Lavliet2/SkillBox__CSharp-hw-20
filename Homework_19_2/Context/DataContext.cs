using Homework_20.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Homework_20.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
                DataBase=Homework_20;
                Trusted_Connection=True;"
                );
        }
    }
}

