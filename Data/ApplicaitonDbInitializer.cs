using Microsoft.AspNetCore.Identity;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.Identity;

namespace RopeyDVDManagementSystem.Data
{
    public class ApplicaitonDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Seeding roles to the initial database
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

                if (!await roleManager.RoleExistsAsync(UserRoles.Assistant))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Assistant));


                // Seeding users to the initial database
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                
                string adminUserEmail = "admin@ropey.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FirstName="Ropey",
                        LastName="Admin",
                        UserName = "admin",
                        Email = adminUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAdminUser, "Admin!123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Manager);
                }

                string appUserEmail = "user@ropey.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FirstName = "Ropey",
                        LastName = "User",
                        UserName = "customer",
                        Email = appUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAppUser, "User!123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Assistant);
                }

            }
        }

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                var image_url = "images/";


                //DVDCategory
                if (!context.DVDCategories.Any())
                {
                    context.DVDCategories.AddRange(new List<DVDCategory>()
                    
                    {

                        new DVDCategory()
                        {
                            CategoryName = "Comedy",
                            CategoryDescription = "This is category for Comedy.",
                            AgeRestricted = false
                        },

                        new DVDCategory()
                        {
                            CategoryName = "Drama",
                            CategoryDescription = "This is category for Drama.",
                            AgeRestricted = false
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Thriller",
                            CategoryDescription = "This is category for Thriller.",
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Horror",
                            CategoryDescription = "This is category for Horror.",
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Romance",
                            CategoryDescription = "This is category for Romance.",
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Science-Fiction",
                            CategoryDescription = "This is category for Science-Fiction.",
                            AgeRestricted = false
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Fantasy",
                            CategoryDescription = "This is category for Fantasy.",
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Action",
                            CategoryDescription = "This is category for Action.",
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Crime",
                            CategoryDescription = "This is category for Crime.",
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Sport",
                            CategoryDescription = "This is category for Sports.",
                            AgeRestricted = false
                        }
                    });

                    context.SaveChanges();

                }


                //Producer
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            ProducerName = "Taylor Congdon"
                        },
                        new Producer()
                        {
                            ProducerName = "Fede Alvarez"
                        },
                        new Producer()
                        {
                            ProducerName = "Bill Blake"
                        },
                        new Producer()
                        {
                            ProducerName = "David Mitchell"
                        },
                        new Producer()
                        {
                            ProducerName = "Erik Hemmendorff"
                        },
                        new Producer()
                        {
                            ProducerName = "Joshua Beirne-Golden"
                        },
                        new Producer()
                        {
                            ProducerName = "Mary Parent"
                        },
                        new Producer()
                        {
                            ProducerName = "Todd Phillips"
                        },
                        new Producer()
                        {
                            ProducerName = "J.J. Abrams"
                        },
                        new Producer()
                        {
                            ProducerName = "Kevin Feige"
                        },
                        new Producer()
                        {
                            ProducerName = "Christopher Nolan"
                        },
                        new Producer()
                        {
                            ProducerName = "Basil Iwanyk"
                        }
                    });

                    context.SaveChanges();
                }

                //Studio
                if (!context.Studios.Any())
                {
                    context.Studios.AddRange(new List<Studio>()
                    {
                        new Studio()
                        {
                            StudioName = "ThirdEye Pictures"

                        },
                        new Studio()
                        {
                            StudioName = "Half Moon Entertainment"

                        },
                        new Studio()
                        {
                            StudioName = "Paramount Pictures"

                        },
                        new Studio()
                        {
                            StudioName = "Universal Pictures"

                        },
                        new Studio()
                        {
                            StudioName = "Warner Bros. Entertainment"

                        },
                        new Studio()
                        {
                            StudioName = "Sony Pictures Entertainment"

                        },
                        new Studio()
                        {
                            StudioName = "Lucasfilm"

                        },
                        new Studio()
                        {
                            StudioName = "MGM"

                        },
                        new Studio()
                        {
                            StudioName = "LionsGate Entertainment"

                        },
                        new Studio()
                        {
                            StudioName = "Entertainment One"

                        }

                    });

                    context.SaveChanges();

                }

                //DVDTitle
                if (!context.DVDTitles.Any())
                {
                    context.DVDTitles.AddRange(new List<DVDTitle>()
                    {
                        new DVDTitle()
                        {
                            DVDTitleName = "Pumpkinhead",
                            CategoryNumber = 4,
                            StudioNumber = 10,
                            ProducerNumber = 1,
                            DateReleased = DateTime.ParseExact("1989-01-13","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "pumpkinhead.jpeg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.15m
                        },

                        new DVDTitle()
                        {
                            DVDTitleName = "Moto8",
                            CategoryNumber = 10,
                            StudioNumber = 1,
                            ProducerNumber = 2,
                            DateReleased = DateTime.ParseExact("2020-03-05","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "moto8.jpeg"),
                            StandardCharge = 1.5m,
                            PenaltyCharge = 0.10m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Avengers: Endgame",
                            CategoryNumber = 6,
                            StudioNumber = 2,
                            ProducerNumber = 3,
                            DateReleased = DateTime.ParseExact("2019-04-26","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "avengers.jpg"),
                            StandardCharge = 3,
                            PenaltyCharge = 0.25m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Star Wars: Episode IX",
                            CategoryNumber = 6,
                            StudioNumber = 7,
                            ProducerNumber = 4,
                            DateReleased = DateTime.ParseExact("2019-12-20","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "starwars9.jpg"),
                            StandardCharge = 2.5m,
                            PenaltyCharge = 0.15m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Joker",
                            CategoryNumber = 3,
                            StudioNumber = 3,
                            ProducerNumber = 6,
                            DateReleased = DateTime.ParseExact("2019-10-04","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "joker.jpg"),
                            StandardCharge = 1.5m,
                            PenaltyCharge = 0.10m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Inception",
                            CategoryNumber = 3,
                            StudioNumber = 4,
                            ProducerNumber = 5,
                            DateReleased = DateTime.ParseExact("2010-07-08","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "inception.jpg"),
                            StandardCharge = 2.5m,
                            PenaltyCharge = 0.20m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "John Wick",
                            CategoryNumber = 9,
                            StudioNumber = 5,
                            ProducerNumber = 8,
                            DateReleased = DateTime.ParseExact("2014-10-24","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "johnwick.jpg"),
                            StandardCharge = 1.5m,
                            PenaltyCharge = 0.15m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Dune",
                            CategoryNumber = 6,
                            StudioNumber = 6,
                            ProducerNumber = 7,
                            DateReleased = DateTime.ParseExact("2021-10-22","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "dune.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.15m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Kicks",
                            CategoryNumber = 2,
                            StudioNumber = 8,
                            ProducerNumber = 1,
                            DateReleased = DateTime.ParseExact("2016-07-01","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "kicks.jpeg"),
                            StandardCharge = 1,
                            PenaltyCharge = 0.05m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Pleasure",
                            CategoryNumber = 7,
                            StudioNumber = 10,
                            ProducerNumber = 8,
                            DateReleased = DateTime.ParseExact("2021-10-20","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "pleasure.jpeg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.10m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Dont Breathe",
                            CategoryNumber = 4,
                            StudioNumber = 9,
                            ProducerNumber = 1,
                            DateReleased = DateTime.ParseExact("2016-03-12","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "dontbreathe.jpeg"),
                            StandardCharge = 1,
                            PenaltyCharge = 0.10m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Palm Springs",
                            CategoryNumber = 1,
                            StudioNumber = 1,
                            ProducerNumber = 9,
                            DateReleased = DateTime.ParseExact("2021-03-04","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "palmsprings.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.15m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Black Widow",
                            CategoryNumber = 8,
                            StudioNumber = 6,
                            ProducerNumber = 10,
                            DateReleased = DateTime.ParseExact("2021-07-09","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "blackwidow.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.20m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "The Shawshank Redemption",
                            CategoryNumber = 2,
                            StudioNumber = 5,
                            ProducerNumber = 4,
                            DateReleased = DateTime.ParseExact("1994-09-22","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "shawshankredemption.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.20m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Jurassic Park",
                            CategoryNumber = 3,
                            StudioNumber = 3,
                            ProducerNumber = 7,
                            DateReleased = DateTime.ParseExact("1993-06-09","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "jurassicpark.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.15m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Backdraft",
                            CategoryNumber = 3,
                            StudioNumber = 2,
                            ProducerNumber = 6,
                            DateReleased = DateTime.ParseExact("1991-05-24","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "backdraft.jpeg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.15m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "IT Follows",
                            CategoryNumber = 4,
                            StudioNumber = 8,
                            ProducerNumber = 2,
                            DateReleased = DateTime.ParseExact("2015-03-27","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "itfollows.jpeg"),
                            StandardCharge = 1.5m,
                            PenaltyCharge = 0.05m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Titanic",
                            CategoryNumber = 5,
                            StudioNumber = 9,
                            ProducerNumber = 3,
                            DateReleased = DateTime.ParseExact("1997-12-17","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "titanic.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.10m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "The Batman",
                            CategoryNumber = 8,
                            StudioNumber = 5,
                            ProducerNumber = 2,
                            DateReleased = DateTime.ParseExact("2022-03-04","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "thebatman.jpg"),
                            StandardCharge = 3,
                            PenaltyCharge = 0.30m
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Fight Club",
                            CategoryNumber = 3,
                            StudioNumber = 10,
                            ProducerNumber = 10,
                            DateReleased = DateTime.ParseExact("1999-09-11","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "fightclub.jpg"),
                            StandardCharge = 2.5m,
                            PenaltyCharge = 0.25m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Doctor Strange in the Multiverse of Madness",
                            CategoryNumber = 6,
                            StudioNumber = 1,
                            ProducerNumber = 10,
                            DateReleased = DateTime.ParseExact("2022-05-06","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "drstrange2.jpg"),
                            StandardCharge = 5,
                            PenaltyCharge = 1.25m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "La La Land",
                            CategoryNumber = 5,
                            StudioNumber = 2,
                            ProducerNumber = 9,
                            DateReleased = DateTime.ParseExact("2016-12-09","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "lalaland.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.20m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Spider-Man: No Way Home",
                            CategoryNumber = 6,
                            StudioNumber = 3,
                            ProducerNumber = 8,
                            DateReleased = DateTime.ParseExact("2021-12-17","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "spidermannwh.jpg"),
                            StandardCharge = 3.5m,
                            PenaltyCharge = 0.75m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Lord of the Rings: The Two Towers",
                            CategoryNumber = 3,
                            StudioNumber = 4,
                            ProducerNumber = 7,
                            DateReleased = DateTime.ParseExact("2002-12-18","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "lotr2.jpg"),
                            StandardCharge = 2.5m,
                            PenaltyCharge = 0.25m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Captain",
                            CategoryNumber = 10,
                            StudioNumber = 5,
                            ProducerNumber = 6,
                            DateReleased = DateTime.ParseExact("2019-03-01","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "captain.jpg"),
                            StandardCharge = 4,
                            PenaltyCharge = 0.80m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "The Dark Knight",
                            CategoryNumber = 9,
                            StudioNumber = 6,
                            ProducerNumber = 11,
                            DateReleased = DateTime.ParseExact("2008-07-18","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "thedarkknight.jpg"),
                            StandardCharge = 3,
                            PenaltyCharge = 0.40m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Pirates of the Caribbean",
                            CategoryNumber = 8,
                            StudioNumber = 7,
                            ProducerNumber = 4,
                            DateReleased = DateTime.ParseExact("2003-06-28","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "piratesofthecaribbean.jpg"),
                            StandardCharge = 3.5m,
                            PenaltyCharge = 0.75m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Shutter Island",
                            CategoryNumber = 3,
                            StudioNumber = 8,
                            ProducerNumber = 11,
                            DateReleased = DateTime.ParseExact("2010-02-19","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "shutterisland.jpg"),
                            StandardCharge = 2.5m,
                            PenaltyCharge = 0.25m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "Bohemian Rhapsody",
                            CategoryNumber = 2,
                            StudioNumber = 9,
                            ProducerNumber = 2,
                            DateReleased = DateTime.ParseExact("2018-10-24","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "bohemianrhapsody.jpg"),
                            StandardCharge = 2,
                            PenaltyCharge = 0.15m,
                        },
                        new DVDTitle()
                        {
                            DVDTitleName = "14 Peaks",
                            CategoryNumber = 10,
                            StudioNumber = 10,
                            ProducerNumber = 1,
                            DateReleased = DateTime.ParseExact("2021-11-29","yyyy-MM-dd",null),
                            DVDPoster = string.Concat(image_url, "14peaks.jpg"),
                            StandardCharge = 4,
                            PenaltyCharge = 0.75m,
                        }

                     });

                    context.SaveChanges();

                }

                //Actor
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            ActorFirstName = "Keanu",
                            ActorSurName = "Reeves"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Scarlett",
                            ActorSurName = "Johansson"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Robert",
                            ActorSurName = "Downey Jr."

                        },
                        new Actor()
                        {
                            ActorFirstName = "Brad",
                            ActorSurName = "Pitt"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Leonardo",
                            ActorSurName = "Di Caprio"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Chris",
                            ActorSurName = "Hemsworth"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Morgan",
                            ActorSurName = "Freeman"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Robert",
                            ActorSurName = "Pattinson"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Mark",
                            ActorSurName = "Hamil"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Lance",
                            ActorSurName = "Henriksen"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Haiden",
                            ActorSurName = "Deegan"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Cristin",
                            ActorSurName = "Milioti"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Andy",
                            ActorSurName = "Samberg"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Maika",
                            ActorSurName = "Monroe"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Laura",
                            ActorSurName = "Dern"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Jaoquin",
                            ActorSurName = "Phoenix"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Timothee",
                            ActorSurName = "Chalamet"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Sofia",
                            ActorSurName = "Kappel"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Stephen",
                            ActorSurName = "Lang"

                        },
                        new Actor()
                        {
                            ActorFirstName = "William",
                            ActorSurName = "Baldwin"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Chris",
                            ActorSurName = "Hemsworth"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Jahking",
                            ActorSurName = "Guillory"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Jared",
                            ActorSurName = "Leto"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Tom",
                            ActorSurName = "Hardy"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Zoe",
                            ActorSurName = "Kravitz"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Benedict",
                            ActorSurName = "Cumberbatch"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Ryan",
                            ActorSurName = "Gosling"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Tom",
                            ActorSurName = "Holland"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Elizabeth",
                            ActorSurName = "Olsen"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Andrew",
                            ActorSurName = "Garfield"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Tobey",
                            ActorSurName = "Maguire"

                        },
                        new Actor()
                        {
                            ActorFirstName = "",
                            ActorSurName = ""

                        },
                        new Actor()
                        {
                            ActorFirstName = "Anmol",
                            ActorSurName = "KC"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Christian",
                            ActorSurName = "Bale"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Johnny",
                            ActorSurName = "Depp"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Rami",
                            ActorSurName = "Malek"

                        },
                        new Actor()
                        {
                            ActorFirstName = "Nims",
                            ActorSurName = "Purja"

                        }
                    });

                    context.SaveChanges();

                }


                //CastMember
                if (!context.CastMembers.Any())
                {
                    context.CastMembers.AddRange(new List<CastMember>()
                    {
                        new CastMember()
                        {
                            DVDNumber = 1,
                            ActorNumber = 10

                        },
                        new CastMember()
                        {
                            DVDNumber = 2,
                            ActorNumber = 11

                        },
                        new CastMember()
                        {
                            DVDNumber = 3,
                            ActorNumber = 2

                        },
                        new CastMember()
                        {
                            DVDNumber = 3,
                            ActorNumber = 3

                        },
                        new CastMember()
                        {
                            DVDNumber = 3,
                            ActorNumber = 6

                        },
                        new CastMember()
                        {
                            DVDNumber = 4,
                            ActorNumber = 9

                        },
                        new CastMember()
                        {
                            DVDNumber = 5,
                            ActorNumber = 16

                        },
                        new CastMember()
                        {
                            DVDNumber = 6,
                            ActorNumber = 5

                        },
                        new CastMember()
                        {
                            DVDNumber = 7,
                            ActorNumber = 1

                        },
                        new CastMember()
                        {
                            DVDNumber = 8,
                            ActorNumber = 17

                        },
                        new CastMember()
                        {
                            DVDNumber = 9,
                            ActorNumber = 22

                        },
                        new CastMember()
                        {
                            DVDNumber = 10,
                            ActorNumber = 18

                        },
                        new CastMember()
                        {
                            DVDNumber = 11,
                            ActorNumber = 19

                        },
                        new CastMember()
                        {
                            DVDNumber = 12,
                            ActorNumber = 12

                        },
                        new CastMember()
                        {
                            DVDNumber = 12,
                            ActorNumber = 13

                        },
                        new CastMember()
                        {
                            DVDNumber = 13,
                            ActorNumber = 2

                        },
                        new CastMember()
                        {
                            DVDNumber = 14,
                            ActorNumber = 7

                        },
                        new CastMember()
                        {
                            DVDNumber = 15,
                            ActorNumber = 15

                        },
                        new CastMember()
                        {
                            DVDNumber = 16,
                            ActorNumber = 20

                        },
                        new CastMember()
                        {
                            DVDNumber = 17,
                            ActorNumber = 14

                        },
                        new CastMember()
                        {
                            DVDNumber = 18,
                            ActorNumber = 5

                        },
                        new CastMember()
                        {
                            DVDNumber = 19,
                            ActorNumber = 8

                        },
                        new CastMember()
                        {
                            DVDNumber = 19,
                            ActorNumber = 25

                        },
                        new CastMember()
                        {
                            DVDNumber = 20,
                            ActorNumber = 4

                        },
                        new CastMember()
                        {
                            DVDNumber = 20,
                            ActorNumber = 23

                        },
                        new CastMember()
                        {
                            DVDNumber = 21,
                            ActorNumber = 26

                        },
                        new CastMember()
                        {
                            DVDNumber = 21,
                            ActorNumber = 29

                        },
                        new CastMember()
                        {
                            DVDNumber = 22,
                            ActorNumber = 27

                        },
                        new CastMember()
                        {
                            DVDNumber = 23,
                            ActorNumber = 28

                        },
                        new CastMember()
                        {
                            DVDNumber = 23,
                            ActorNumber = 30

                        },
                        new CastMember()
                        {
                            DVDNumber = 23,
                            ActorNumber = 31

                        },
                        new CastMember()
                        {
                            DVDNumber = 24,
                            ActorNumber = 32

                        },
                        new CastMember()
                        {
                            DVDNumber = 25,
                            ActorNumber = 33

                        },
                        new CastMember()
                        {
                            DVDNumber = 26,
                            ActorNumber = 34

                        },
                        new CastMember()
                        {
                            DVDNumber = 27,
                            ActorNumber = 35

                        },
                        new CastMember()
                        {
                            DVDNumber = 28,
                            ActorNumber = 5

                        },
                        new CastMember()
                        {
                            DVDNumber = 29,
                            ActorNumber = 36

                        },
                        new CastMember()
                        {
                            DVDNumber = 30,
                            ActorNumber = 37

                        }

                    });

                    context.SaveChanges();
                }

                //DVDCopy
                if (!context.DVDCopies.Any())
                {
                    context.DVDCopies.AddRange(new List<DVDCopy>()
                    {
                        new DVDCopy()
                        {
                            DVDNumber = 1,
                            DatePurchased = DateTime.ParseExact("2022-02-11","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 2,
                            DatePurchased = DateTime.ParseExact("2022-02-10","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 3,
                            DatePurchased = DateTime.ParseExact("2022-02-01","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 4,
                            DatePurchased = DateTime.ParseExact("2022-02-12","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 5,
                            DatePurchased = DateTime.ParseExact("2022-02-15","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 6,
                            DatePurchased = DateTime.ParseExact("2022-01-17","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 7,
                            DatePurchased = DateTime.ParseExact("2022-01-20","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 8,
                            DatePurchased = DateTime.ParseExact("2022-02-20","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 9,
                            DatePurchased = DateTime.ParseExact("2022-01-12","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 10,
                            DatePurchased = DateTime.ParseExact("2022-01-01","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 11,
                            DatePurchased = DateTime.ParseExact("2022-01-08","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 12,
                            DatePurchased = DateTime.ParseExact("2022-01-25","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 13,
                            DatePurchased = DateTime.ParseExact("2022-02-04","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 15,
                            DatePurchased = DateTime.ParseExact("2022-02-10","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 16,
                            DatePurchased = DateTime.ParseExact("2022-01-29","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 17,
                            DatePurchased = DateTime.ParseExact("2022-01-27","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 18,
                            DatePurchased = DateTime.ParseExact("2022-01-21","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 19,
                            DatePurchased = DateTime.ParseExact("2022-02-09","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 20,
                            DatePurchased = DateTime.ParseExact("2022-01-15","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 20,
                            DatePurchased = DateTime.ParseExact("2022-01-15","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 20,
                            DatePurchased = DateTime.ParseExact("2022-01-16","yyyy-MM-dd",null),
                            IsOnLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 21,
                            DatePurchased = DateTime.ParseExact("2021-08-16","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 22,
                            DatePurchased = DateTime.ParseExact("2021-08-26","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 23,
                            DatePurchased = DateTime.ParseExact("2021-07-09","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 24,
                            DatePurchased = DateTime.ParseExact("2021-09-11","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 25,
                            DatePurchased = DateTime.ParseExact("2021-09-10","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 26,
                            DatePurchased = DateTime.ParseExact("2021-10-26","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 27,
                            DatePurchased = DateTime.ParseExact("2021-09-09","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 28,
                            DatePurchased = DateTime.ParseExact("2021-08-22","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 29,
                            DatePurchased = DateTime.ParseExact("2021-10-29","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 30,
                            DatePurchased = DateTime.ParseExact("2021-12-25","yyyy-MM-dd",null),
                            IsOnLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 30,
                            DatePurchased = DateTime.ParseExact("2021-12-26","yyyy-MM-dd",null),
                            IsOnLoan = true
                        }



                    });

                    context.SaveChanges();

                }

                //MembershipCategory
                if (!context.MembershipCategories.Any())
                {
                    context.MembershipCategories.AddRange(new List<MembershipCategory>()
                    {
                        new MembershipCategory()
                        {
                            MembershipCategoryName = "Basic",
                            MembershipCategoryDescription = "This is our most basic membership plan where you can loan 3 DVDs.",
                            MembershipCategoryTotalLoans = 3

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryName = "Standard",
                            MembershipCategoryDescription = "This is our standard membership plan where you can loan 5 DVDss.",
                            MembershipCategoryTotalLoans = 5

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryName = "Medium",
                            MembershipCategoryDescription = "This is our medium membership plan where you can loan 7 DVDs.",
                            MembershipCategoryTotalLoans = 7

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryName = "Premium",
                            MembershipCategoryDescription = "This is our premium membership plan where you can loan 10 DVDs",
                            MembershipCategoryTotalLoans = 10

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryName = "Ultimate",
                            MembershipCategoryDescription = "This is our ultimate membership plan where you can loan 15 DVDs",
                            MembershipCategoryTotalLoans = 15

                        },

                    });

                    context.SaveChanges();

                }


                //Member
                if (!context.Members.Any())
                {
                    context.Members.AddRange(new List<Member>()
                    {
                        new Member()
                        {
                            MembershipCategoryNumber = 4,
                            MemberFirstName = "Abhishek",
                            MemberLastName = "Rokaya",
                            MemberAddress = "Samakhusi, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2001-04-15","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 2,
                            MemberFirstName = "Ushaan",
                            MemberLastName = "Shrestha",
                            MemberAddress = "Jyatha, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2000-10-07","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 1,
                            MemberFirstName = "Oshriya",
                            MemberLastName = "Manandhar",
                            MemberAddress = "Budhanilkantha, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2001-06-25","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 5,
                            MemberFirstName = "Animesh",
                            MemberLastName = "Basnet",
                            MemberAddress = "Satdobato, Lalitpur",
                            MemberDateOfBirth = DateTime.ParseExact("1998-08-20","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 2,
                            MemberFirstName = "Pranamya",
                            MemberLastName = "Sharma",
                            MemberAddress = "Naikap, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2002-05-29","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 1,
                            MemberFirstName = "Prabhas",
                            MemberLastName = "Khanal",
                            MemberAddress = "Thimi, Bhaktapur",
                            MemberDateOfBirth = DateTime.ParseExact("1996-12-25","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 3,
                            MemberFirstName = "Isabella",
                            MemberLastName = "Gurung",
                            MemberAddress = "Patan, Lalitpur",
                            MemberDateOfBirth = DateTime.ParseExact("2006-10-05","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 2,
                            MemberFirstName = "Bharat",
                            MemberLastName = "Humagain",
                            MemberAddress = "Tangal, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2008-11-29","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 5,
                            MemberFirstName = "Ishan",
                            MemberLastName = "Bajracharya",
                            MemberAddress = "Thamel, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("1997-08-28","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MembershipCategoryNumber = 4,
                            MemberFirstName = "Rochak",
                            MemberLastName = "Dahal",
                            MemberAddress = "Lubhu, Lalitpur",
                            MemberDateOfBirth = DateTime.ParseExact("2000-07-21","yyyy-MM-dd",null)

                        }

                    });

                    context.SaveChanges();

                }


                //LoanType
                if (!context.LoanTypes.Any())
                {
                    context.LoanTypes.AddRange(new List<LoanType>()
                    {
                        new LoanType()
                        {
                            LoanTypeTitle = "Trebel Trouble",
                            LoanDuration = 3

                        },
                        new LoanType()
                        {
                            LoanTypeTitle = "Five Star",
                            LoanDuration = 5

                        },
                        new LoanType()
                        {
                            LoanTypeTitle = "Weekly",
                            LoanDuration = 7

                        },
                        new LoanType()
                        {
                            LoanTypeTitle = "New Moon",
                            LoanDuration = 15

                        },
                        new LoanType()
                        {
                            LoanTypeTitle = "Whole Month",
                            LoanDuration = 30

                        }

                    });

                    context.SaveChanges();

                }

                //Loan
                if (!context.Loans.Any())
                {
                    context.Loans.AddRange(new List<Loan>()
                    {
                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 3,
                            MemberNumber = 7,
                            DateOut = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-25","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-24","yyyy-MM-dd",null),
                            ReturnAmount = 15
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 13,
                            MemberNumber = 2,
                            DateOut = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-18","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-18","yyyy-MM-dd",null),
                            ReturnAmount = 6
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 16,
                            MemberNumber = 10,
                            DateOut = DateTime.ParseExact("2022-04-10","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-16","yyyy-MM-dd",null),
                            ReturnAmount = 14
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 5,
                            CopyNumber = 1,
                            MemberNumber = 9,
                            DateOut = DateTime.ParseExact("2022-03-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-16","yyyy-MM-dd",null),
                            ReturnAmount = 60
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 4,
                            CopyNumber = 8,
                            MemberNumber = 1,
                            DateOut = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-30","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            ReturnAmount = 30
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 12,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            ReturnAmount = 10
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 20,
                            MemberNumber = 8,
                            DateOut = DateTime.ParseExact("2022-04-27","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-30","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            ReturnAmount = 7.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 5,
                            CopyNumber = 6,
                            MemberNumber = 5,
                            DateOut = DateTime.ParseExact("2022-03-17","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null),
                            ReturnAmount = 75
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 17,
                            MemberNumber = 6,
                            DateOut = DateTime.ParseExact("2022-04-21","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-28","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-26","yyyy-MM-dd",null),
                            ReturnAmount = 10.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 9,
                            MemberNumber = 4,
                            DateOut = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-30","yyyy-MM-dd",null),
                            ReturnAmount = 3
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 15,
                            MemberNumber = 10,
                            DateOut = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),
                            ReturnAmount = 6

                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 1,
                            MemberNumber = 7,
                            DateOut = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),
                            ReturnAmount = 6
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 2,
                            MemberNumber = 8,
                            DateOut = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),
                            ReturnAmount = 4.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 17,
                            MemberNumber = 6,
                            DateOut = DateTime.ParseExact("2022-04-21","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-28","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-26","yyyy-MM-dd",null),
                            ReturnAmount = 10.5m
                        },


                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 4,
                            MemberNumber = 1,
                            DateOut = DateTime.ParseExact("2022-03-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-04","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-03-04","yyyy-MM-dd",null),
                            ReturnAmount = 7.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 5,
                            MemberNumber = 2,
                            DateOut = DateTime.ParseExact("2022-03-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-18","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-03-20","yyyy-MM-dd",null),
                            ReturnAmount = 4.7m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 7,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-02-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-02-25","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-02-23","yyyy-MM-dd",null),
                            ReturnAmount = 7.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 10,
                            MemberNumber = 4,
                            DateOut = DateTime.ParseExact("2022-02-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-02-20","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-02-21","yyyy-MM-dd",null),
                            ReturnAmount = 10.10m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 11,
                            MemberNumber = 5,
                            DateOut = DateTime.ParseExact("2022-03-16","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-23","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-03-22","yyyy-MM-dd",null),
                            ReturnAmount = 7
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 14,
                            MemberNumber = 6,
                            DateOut = DateTime.ParseExact("2022-03-21","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-28","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-03-27","yyyy-MM-dd",null),
                            ReturnAmount = 14
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 4,
                            CopyNumber = 18,
                            MemberNumber = 7,
                            DateOut = DateTime.ParseExact("2022-03-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-16","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-03-14","yyyy-MM-dd",null),
                            ReturnAmount = 45
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 4,
                            CopyNumber = 19,
                            MemberNumber = 8,
                            DateOut = DateTime.ParseExact("2022-03-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-30","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-03-28","yyyy-MM-dd",null),
                            ReturnAmount = 37.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 5,
                            CopyNumber = 21,
                            MemberNumber = 9,
                            DateOut = DateTime.ParseExact("2022-03-14","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-14","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-10","yyyy-MM-dd",null),
                            ReturnAmount = 75
                        },

 
                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 22,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-05-05","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-08","yyyy-MM-dd",null),
                            ReturnAmount = 15
                        },
                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 23,
                            MemberNumber = 10,
                            DateOut = DateTime.ParseExact("2022-05-06","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-09","yyyy-MM-dd",null),
                            ReturnAmount = 6
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 24,
                            MemberNumber = 9,
                            DateOut = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-07","yyyy-MM-dd",null),
                            ReturnAmount = 17.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 25,
                            MemberNumber = 8,
                            DateOut = DateTime.ParseExact("2022-05-03","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-08","yyyy-MM-dd",null),
                            ReturnAmount = 12.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 26,
                            MemberNumber = 7,
                            DateOut = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-08","yyyy-MM-dd",null),
                            ReturnAmount = 28
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 27,
                            MemberNumber = 6,
                            DateOut = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-09","yyyy-MM-dd",null),
                            ReturnAmount = 21
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 4,
                            CopyNumber = 28,
                            MemberNumber = 5,
                            DateOut = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-05","yyyy-MM-dd",null),
                            ReturnAmount = 52.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 4,
                            CopyNumber = 29,
                            MemberNumber = 4,
                            DateOut = DateTime.ParseExact("2022-04-28","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-13","yyyy-MM-dd",null),
                            ReturnAmount = 37.5m
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 5,
                            CopyNumber = 30,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-04-05","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-05","yyyy-MM-dd",null),
                            ReturnAmount = 60
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 5,
                            CopyNumber = 31,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-20","yyyy-MM-dd",null),
                            ReturnAmount = 120
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 32,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-05-04","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-09","yyyy-MM-dd",null),
                            ReturnAmount = 20
                        }

                    });

                    context.SaveChanges();

                }

            }
        }
    }
}
