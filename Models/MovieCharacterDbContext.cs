using AssignmentWebAPI.Models.Domain;
using AssignmentWebAPI.Models.DummyData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AssignmentWebAPI.Models
{
    public class MovieCharacterDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public MovieCharacterDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(SeedData.SeedCharacters());
            modelBuilder.Entity<Movie>().HasData(SeedData.SeedMovies());
            modelBuilder.Entity<Franchise>().HasData(SeedData.SeedFranchise());

            modelBuilder.Entity<Movie>()
           .HasMany(p => p.Characters)
           .WithMany(m => m.Movies)
           .UsingEntity<Dictionary<string, object>>(
               "CharactersMovie",
               r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
               l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
               je =>
               {
                   je.HasKey("MovieId", "CharacterId");
                   je.HasData(
                       new { MovieId = 1, CharacterId = 1 },
                       new { MovieId = 2, CharacterId = 1 },
                       new { MovieId = 2, CharacterId = 2 },
                       new { MovieId = 3, CharacterId = 1 },
                       new { MovieId = 4, CharacterId = 3 },
                       new { MovieId = 4, CharacterId = 4 },
                       new { MovieId = 4, CharacterId = 5 },
                       new { MovieId = 5, CharacterId = 5 },
                       new { MovieId = 6, CharacterId = 3 },
                       new { MovieId = 6, CharacterId = 4 },
                       new { MovieId = 6, CharacterId = 5 },
                       new { MovieId = 7, CharacterId = 6 },
                       new { MovieId = 8, CharacterId = 6 }

                   );
               });

        }
    }
}
