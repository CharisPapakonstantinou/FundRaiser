using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Common.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<Media> Media { get; set; }

        public DbSet<Fund> Funds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Initial Catalog = 'FundRaiser'; User Id = SA; Password=123456Ab");
        }
        
        public AppDbContext() : base() { }
        
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             //User - Reward many to many relationship (via Funds as intermediate table)
              modelBuilder.Entity<Fund>().HasKey(f => new { f.UserId, f.RewardId });

              modelBuilder.Entity<Fund>()
                  .HasOne(f => f.User)
                  .WithMany(p => p.Funds)
                  .OnDelete(DeleteBehavior.NoAction);
              
              modelBuilder.Entity<Fund>()
                  .HasOne(f => f.Reward)
                  .WithMany(r => r.Funds)
                  .OnDelete(DeleteBehavior.NoAction);
              
              //Project - Updates one to many relationship
              modelBuilder.Entity<Project>()
                  .HasMany(p => p.Updates)
                  .WithOne(u => u.Project);
              
              //Project - Reward one to many relationship
              modelBuilder.Entity<Project>()
                  .HasMany(p => p.Rewards)
                  .WithOne(u => u.Project);
              
              //Project - User many to one relationship
              modelBuilder.Entity<Project>()
                  .HasOne(p => p.User)
                  .WithMany(u => u.Projects);
              
              //Project - Media one to many relationship
              modelBuilder.Entity<Project>()
                  .HasMany(p => p.Media)
                  .WithOne(m => m.Project);
        }
    }
}