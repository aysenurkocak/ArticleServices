using Microsoft.EntityFrameworkCore;
using PocoLib;

/**
 * DBContext
 * - Db Connection, table tanımı 
 * @author  Ayşe Nur 
 * @version 1.0
 * @CreatedDate 07.04.2020
 */

namespace EFLibCore

{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=localhost;initial catalog=ArticleDB;integrated security=True;");
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Log>().ToTable("Log");
        }
        
    }
}
