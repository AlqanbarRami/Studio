
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.User;
using System;

namespace MoviesApi.Persistence
{
    public class AppDbContext : DbContext
    {
        
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<FilmStudio> FilmStudios { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>().HasData(new Film
            {
                FilmId = 1,
                Country = "France",
                Director = "Jean-Paul Salomé",
                Name = "Mama Weed",
                ReleaseDate = "2020"

            });

            modelBuilder.Entity<Film>().HasData(new Film
            {
                FilmId = 2,
                Country = "Sweden",
                Director = "Lars Dimming, Bo Harringer",
                Name = "Den siste cafépianisten",
                ReleaseDate = "2012"

            });

            modelBuilder.Entity<Film>().HasData(new Film
            {
                FilmId = 3,
                Country = "USA",
                Director = "Joel Coen",
                Name = "The Tragedy of Macbeth",
                ReleaseDate = "2021"

            });

            modelBuilder.Entity<Film>().HasData(new Film
            {
                FilmId = 4,
                Country = "Sweden",
                Director = "Jesper Klevenås",
                Name = "Shop",
                ReleaseDate = "2020"

            });

            modelBuilder.Entity<Film>().HasData(new Film
            {
                FilmId = 5,
                Country = "Malta",
                Director = "Alex Camilleri",
                Name = "Luzzu",
                ReleaseDate = "2021"

            });

            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 1,
                City = "Holllywood",
                Name = "MGM",
               

            });

            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 2,
                City = "Hollywood",
                Name = "News Corporation",

            });
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Role = "admin",
                UserName = "Admin",
                Password = "Admin123!",
                PasswordHash = hasher.HashPassword(null, "Admin123!")

            });
            modelBuilder.Entity<User>().HasData(new User
            {
                FilmStudioId = "1",
                Role = "filmstudio",
                UserName = "Studio",
                Password = "Studio123!",
                PasswordHash = hasher.HashPassword(null, "Studio123!")

            });

            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1,
                FilmId = 1,
                RentedOut = false,
                StudioId = 0
               
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2,
                FilmId = 1,
                RentedOut = false,
                StudioId = 0

            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3,
                FilmId = 2,
                RentedOut = false,
                StudioId = 0

            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 4,
                FilmId = 2,
                RentedOut = false,
                StudioId = 0

            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 5,
                FilmId = 3,
                RentedOut = false,
                StudioId = 0

            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 6,
                FilmId = 4,
                RentedOut = false,
                StudioId = 0

            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 7,
                FilmId = 4,
                RentedOut = false,
                StudioId = 0

            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 8,
                FilmId = 5,
                RentedOut = false,
                StudioId = 0

            });
        }
    }
}
