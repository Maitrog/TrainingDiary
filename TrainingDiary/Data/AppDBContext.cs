using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("DataSource=./Data/AppDb.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>().HasData(Array.Empty<Exercise>());
            modelBuilder.Entity<Set>().HasData(Array.Empty<Set>());
            modelBuilder.Entity<Workout>().HasData(Array.Empty<Workout>());
            modelBuilder.Entity<User>().HasData(Array.Empty<User>());
            modelBuilder.Entity<ExerciseType>().HasData(Array.Empty<ExerciseType>());
            modelBuilder.Entity<MuscleGroup>().HasData(Array.Empty<MuscleGroup>());
            modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole> 
            { 
                new IdentityRole { Name = "user", NormalizedName="USER" }, 
                new IdentityRole { Name = "admin", NormalizedName = "ADMIN" }, 
                new IdentityRole { Name = "moderator", NormalizedName="MODERATOR" } 
            });
        }
        //public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        //{
        //    Database.EnsureCreated();
        //}

    }
}
