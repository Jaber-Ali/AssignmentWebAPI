using AssignmentWebAPI.Models.Domain;
using System;
using System.Collections.Generic;

namespace AssignmentWebAPI.Models.DummyData
{
    public static class SeedData
    {
        public static IEnumerable<Character> SeedCharacters()
        {
            List<Character> characters = new List<Character>();

            characters.Add(new Character()
            {
                Id = 1,
                FullName = "Peter Benjamin Parker",
                Alias = "Peter Parker",
                Gender = "Male",
                Picture = "https://sv.wikipedia.org/wiki/Spider-Man:_No_Way_Home#/media/Fil:No_way_home_logo.png"
            });
            characters.Add(new Character()
            {
                Id = 2,
                FullName = "Harold Osborn",
                Alias = "Green Goblin",
                Gender = "Male",
                Picture = "https://marvel.fandom.com/wiki/Harold_Osborn_(Earth-616)?file=Amazing_Spider-Man_Vol_5_76_Marvel_Masterpieces_Variant_Textless.png"
            });

            characters.Add(new Character()
            {
                Id = 3,
                FullName = "Galahad",
                Alias = "Mr. DeVere",
                Gender = "Male",
                Picture = "https://kingsman.fandom.com/wiki/Harry_Hart?file=Former+Agent+Galahad.jpeg"
            });
            characters.Add(new Character()
            {
                Id = 4,
                FullName = "Hamish Mycroft",
                Alias = "Merlin",
                Gender = "Male",
                Picture = "https://hero.fandom.com/wiki/Merlin_(Kingsman)?file=Kingsman_the_golden_circle_merlin_2_png_by_mintmovi3-dbops4h.png"
            });

            characters.Add(new Character()
            {
                Id = 5,
                FullName = "Gary Unwin",
                Alias = "Eggsy",
                Gender = "Male",
                Picture = "https://hero.fandom.com/wiki/Gary_%22Eggsy%22_Unwin?file=Eggsy_and_the_Orange_Jacket.jpg"
            });
            characters.Add(new Character()
            {
                Id = 6,
                FullName = "Jardni Jovanovich",
                Alias = "Keanu",
                Gender = "Male",
                Picture = "https://en.wikipedia.org/wiki/John_Wick_(character)#/media/File:John_Wick_Keanu.jpeg"
            });
            return characters;
        }

        public static IEnumerable<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>();

            movies.Add(new Movie()
            {
                Id = 1,
                Title = "Spider-Man: No Way Home",
                Genre = "Action/Sci-Fi",
                ReleaseYear = 2021,
                Director = "Jon Watts",
                PictureUrl = "https://sv.wikipedia.org/wiki/Spider-Man:_No_Way_Home#/media/Fil:No_way_home_logo.png",
                Trailer = "https://www.youtube.com/watch?v=JfVOs4VSpmA",
                FranchiseId = 1

            });
            movies.Add(new Movie()
            {
                Id = 2,
                Title = "Spider-Man",
                Genre = "Action/Sci-Fi",
                ReleaseYear = 2004,
                Director = "Sam Raimi",
                PictureUrl = "https://en.wikipedia.org/wiki/Spider-Man_(2002_film)#/media/File:Spider-Man2002Poster.jpg",
                Trailer = "https://www.youtube.com/watch?v=TYMMOjBUPMM",
                FranchiseId = 1

            });
            movies.Add(new Movie()
            {
                Id = 3,
                Title = "Spider-Man 2",
                Genre = "Action/Sci-Fi",
                ReleaseYear = 2004,
                Director = "Avi Arad",
                PictureUrl = "https://sv.wikipedia.org/wiki/Spider-Man_2#/media/Fil:Spider-Man_2-Logo.svg",
                Trailer = "https://www.youtube.com/watch?v=1s9Yln0YwCw",
                FranchiseId = 1

            });
            movies.Add(new Movie()
            {
                Id = 4,
                Title = "Kingsman: The Secret Service",
                Genre = "Action",
                ReleaseYear = 2015,
                Director = "Matthew Vaughn",
                PictureUrl = "https://kingsman.fandom.com/wiki/Kingsman:_The_Secret_Service_(film)?file=Image.jpg",
                Trailer = "https://www.youtube.com/watch?v=kl8F-8tR8to",
                FranchiseId = 2

            });
            movies.Add(new Movie()
            {
                Id = 5,
                Title = "The king's man",
                Genre = "Action",
                ReleaseYear = 2021,
                Director = "Matthew Vaughn",
                PictureUrl = "https://www.deviantart.com/nandha602/art/The-King-s-Man-2021-Movie-Folder-Icon-v5-850141109",
                Trailer = "https://www.youtube.com/watch?v=5zdBG-iGfes",
                FranchiseId = 2

            });
            movies.Add(new Movie()
            {
                Id = 6,
                Title = "Kingsman: The Golden Circle",
                Genre = "Action",
                ReleaseYear = 2017,
                Director = "Matthew Vaughn",
                PictureUrl = "https://sv.wikipedia.org/wiki/Kingsman:_The_Golden_Circle#/media/Fil:Kingsman_the_golden_circle_logo.png",
                Trailer = "https://www.youtube.com/watch?v=6Nxc-3WpMbg",
                FranchiseId = 2

            });
            movies.Add(new Movie()
            {
                Id = 7,
                Title = "John Wick",
                Genre = "Action",
                ReleaseYear = 2014,
                Director = "Chad Stehelski",
                PictureUrl = "https://en.wikipedia.org/wiki/John_Wick_(film)#/media/File:John_Wick_TeaserPoster.jpg",
                Trailer = "https://www.youtube.com/watch?v=2AUmvWm5ZDQ",
                FranchiseId = 3

            });
            movies.Add(new Movie()
            {
                Id = 8,
                Title = "John Wick: Chapter 2",
                Genre = "Action",
                ReleaseYear = 2017,
                Director = "Chad Stehelski",
                PictureUrl = "https://en.wikipedia.org/wiki/John_Wick_(film)#/media/File:John_Wick_TeaserPoster.jpg",
                Trailer = "https://www.youtube.com/watch?v=2AUmvWm5ZDQ",
                FranchiseId = 3

            });
          

            return movies;

        }
        public static IEnumerable<Franchise> SeedFranchise()
        {
            List<Franchise> franchises = new List<Franchise>();

            franchises.Add(new Franchise()
            {
                Id = 1,
                FranchiseName = "Spider-Man Franchise",
                Description = " The fictional character Spider  Man a comic book superhero created by Stan Lee and Steve Ditko and featured" +
                "in Marvel Comics publications, has appeared as a main character in multiple theatrical and made -for-television films. " ,
             

            });
            franchises.Add(new Franchise()
            {
                Id = 2,
                FranchiseName = "Kingsman Franchise",
                Description = "Kingsman is a British American film franchise, consisting of action comedy films, " +
                "that follow the missions of the Kingsman, a fictional secret service organization. " ,
              
       

            });
            franchises.Add(new Franchise()
            {
                Id = 3,
                FranchiseName = "John Wick Franchise",
                Description = "The story focuses on John Wick (Reeves) searching for the men who broke into his home, stole his vintage car and killed his puppy, " +
                   "which was a last gift to him from his recently deceased wife...",

            });


            return franchises;
        }

    }
}
