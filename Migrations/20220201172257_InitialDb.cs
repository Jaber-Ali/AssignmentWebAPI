using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentWebAPI.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FranchiseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharactersMovie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharactersMovie", x => new { x.MovieId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_CharactersMovie_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharactersMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "Picture" },
                values: new object[,]
                {
                    { 1, "Peter Parker", "Peter Benjamin Parker", "Male", "https://sv.wikipedia.org/wiki/Spider-Man:_No_Way_Home#/media/Fil:No_way_home_logo.png" },
                    { 2, "Green Goblin", "Harold Osborn", "Male", "https://marvel.fandom.com/wiki/Harold_Osborn_(Earth-616)?file=Amazing_Spider-Man_Vol_5_76_Marvel_Masterpieces_Variant_Textless.png" },
                    { 3, "Mr. DeVere", "Galahad", "Male", "https://kingsman.fandom.com/wiki/Harry_Hart?file=Former+Agent+Galahad.jpeg" },
                    { 4, "Merlin", "Hamish Mycroft", "Male", "https://hero.fandom.com/wiki/Merlin_(Kingsman)?file=Kingsman_the_golden_circle_merlin_2_png_by_mintmovi3-dbops4h.png" },
                    { 5, "Eggsy", "Gary Unwin", "Male", "https://hero.fandom.com/wiki/Gary_%22Eggsy%22_Unwin?file=Eggsy_and_the_Orange_Jacket.jpg" },
                    { 6, "Keanu", "Jardni Jovanovich", "Male", "https://en.wikipedia.org/wiki/John_Wick_(character)#/media/File:John_Wick_Keanu.jpeg" }
                });

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "Id", "Description", "FranchiseName" },
                values: new object[,]
                {
                    { 1, " The fictional character Spider  Man a comic book superhero created by Stan Lee and Steve Ditko and featuredin Marvel Comics publications, has appeared as a main character in multiple theatrical and made -for-television films. ", "Spider-Man Franchise" },
                    { 2, "Kingsman is a British American film franchise, consisting of action comedy films, that follow the missions of the Kingsman, a fictional secret service organization. ", "Kingsman Franchise" },
                    { 3, "The story focuses on John Wick (Reeves) searching for the men who broke into his home, stole his vintage car and killed his puppy, which was a last gift to him from his recently deceased wife...", "John Wick Franchise" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "PictureUrl", "ReleaseYear", "Title", "Trailer" },
                values: new object[,]
                {
                    { 1, "Jon Watts", 1, "Action/Sci-Fi", "https://sv.wikipedia.org/wiki/Spider-Man:_No_Way_Home#/media/Fil:No_way_home_logo.png", 2021, "Spider-Man: No Way Home", "https://www.youtube.com/watch?v=JfVOs4VSpmA" },
                    { 2, "Sam Raimi", 1, "Action/Sci-Fi", "https://en.wikipedia.org/wiki/Spider-Man_(2002_film)#/media/File:Spider-Man2002Poster.jpg", 2004, "Spider-Man", "https://www.youtube.com/watch?v=TYMMOjBUPMM" },
                    { 3, "Avi Arad", 1, "Action/Sci-Fi", "https://sv.wikipedia.org/wiki/Spider-Man_2#/media/Fil:Spider-Man_2-Logo.svg", 2004, "Spider-Man 2", "https://www.youtube.com/watch?v=1s9Yln0YwCw" },
                    { 4, "Matthew Vaughn", 2, "Action", "https://kingsman.fandom.com/wiki/Kingsman:_The_Secret_Service_(film)?file=Image.jpg", 2015, "Kingsman: The Secret Service", "https://www.youtube.com/watch?v=kl8F-8tR8to" },
                    { 5, "Matthew Vaughn", 2, "Action", "https://www.deviantart.com/nandha602/art/The-King-s-Man-2021-Movie-Folder-Icon-v5-850141109", 2021, "The king's man", "https://www.youtube.com/watch?v=5zdBG-iGfes" },
                    { 6, "Matthew Vaughn", 2, "Action", "https://sv.wikipedia.org/wiki/Kingsman:_The_Golden_Circle#/media/Fil:Kingsman_the_golden_circle_logo.png", 2017, "Kingsman: The Golden Circle", "https://www.youtube.com/watch?v=6Nxc-3WpMbg" },
                    { 7, "Chad Stehelski", 3, "Action", "https://en.wikipedia.org/wiki/John_Wick_(film)#/media/File:John_Wick_TeaserPoster.jpg", 2014, "John Wick", "https://www.youtube.com/watch?v=2AUmvWm5ZDQ" },
                    { 8, "Chad Stehelski", 3, "Action", "https://en.wikipedia.org/wiki/John_Wick_(film)#/media/File:John_Wick_TeaserPoster.jpg", 2017, "John Wick: Chapter 2", "https://www.youtube.com/watch?v=2AUmvWm5ZDQ" }
                });

            migrationBuilder.InsertData(
                table: "CharactersMovie",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 1, 3 },
                    { 3, 4 },
                    { 4, 4 },
                    { 5, 4 },
                    { 5, 5 },
                    { 3, 6 },
                    { 4, 6 },
                    { 5, 6 },
                    { 6, 7 },
                    { 6, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharactersMovie_CharacterId",
                table: "CharactersMovie",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_FranchiseId",
                table: "Movie",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharactersMovie");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Franchise");
        }
    }
}
