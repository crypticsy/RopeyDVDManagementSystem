using Microsoft.AspNetCore.Identity;
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

                
                //DVDCategory
                if (!context.DVDCategories.Any())
                {
                    context.DVDCategories.AddRange(new List<DVDCategory>()
                    {
                        new DVDCategory()
                        {
                            CategoryNumber = 1,
                            CategoryDescription = "Comedy"
                            AgeRestricted = false
                        },

                        new DVDCategory()
                        {
                            CategoryNumber = 2,
                            CategoryDescription = "Drama"
                            AgeRestricted = false
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 3,
                            CategoryDescription = "Thriller"
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 4,
                            CategoryDescription = "Horror"
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 5,
                            CategoryDescription = "Romance"
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 6,
                            CategoryDescription = "Science-Fiction"
                            AgeRestricted = false
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 7,
                            CategoryDescription = "Fantasy"
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 8,
                            CategoryDescription = "Action"
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 9,
                            CategoryDescription = "Crime"
                            AgeRestricted = true
                        },
                        new DVDCategory()
                        {
                            CategoryNumber = 10,
                            CategoryDescription = "Sport"
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
                            ProducerNumber = 1,
                            ProducerName = "Taylor Congdon"
                        },
                        new Producer()
                        {
                            ProducerNumber = 2,
                            ProducerName = "Fede Alvarez"
                        },
                        new Producer()
                        {
                            ProducerNumber = 3,
                            ProducerName = "Bill Blake"
                        },
                        new Producer()
                        {
                            ProducerNumber = 4,
                            ProducerName = "David Mitchell"
                        },
                        new Producer()
                        {
                            ProducerNumber = 5,
                            ProducerName = "Erik Hemmendorff"
                        },
                        new Producer()
                        {
                            ProducerNumber = 6,
                            ProducerName = "Joshua Beirne-Golden"
                        },
                        new Producer()
                        {
                            ProducerNumber = 7,
                            ProducerName = "Mary Parent"
                        },
                        new Producer()
                        {
                            ProducerNumber = 8,
                            ProducerName = "Todd Phillips"
                        },
                        new Producer()
                        {
                            ProducerNumber = 9,
                            ProducerName = "J.J. Abrams"
                        },
                        new Producer()
                        {
                            ProducerNumber = 10,
                            ProducerName = "Kevin Feige"
                        },
                        new Producer()
                        {
                            ProducerNumber = 11,
                            ProducerName = "Christopher Nolan"
                        },
                        new Producer()
                        {
                            ProducerNumber = 12,
                            ProducerName = "Basil Iwanyk"
                        }
                    });

                    context.SaveChanges();
                }

                //Studio
                if(!context.Studios.Any())
                {
                    context.Studios.AddRange(new List<Studio>()
                    {
                        new Studio()
                        {
                            StudioNumber = 1,
                            StudioName = "ThirdEye Pictures"

                        },
                        new Studio()
                        {
                            StudioNumber = 2,
                            StudioName = "Half Moon Entertainment"

                        },
                        new Studio()
                        {
                            StudioNumber = 3,
                            StudioName = "Paramount Pictures"

                        },
                        new Studio()
                        {
                            StudioNumber = 4,
                            StudioName = "Universal Pictures"

                        },
                        new Studio()
                        {
                            StudioNumber = 5,
                            StudioName = "Warner Bros. Entertainment"

                        },
                        new Studio()
                        {
                            StudioNumber = 6,
                            StudioName = "Sony Pictures Entertainment"

                        },
                        new Studio()
                        {
                            StudioNumber = 7,
                            StudioName = "Lucasfilm" 

                        },
                        new Studio()
                        {
                            StudioNumber = 8,
                            StudioName = "MGM"

                        },
                        new Studio()
                        {
                            StudioNumber = 9,
                            StudioName = "LionsGate Entertainment"

                        },
                        new Studio()
                        {
                            StudioNumber = 10,
                            StudioName = "Entertainment One"

                        }

                    });

                    context.SaveChanges();

                }

                //DVDTitle
                if(!context.DVDTitles.Any())
                {
                    context.DVDTitles.AddRange(new List<DVDTitle>()
                    {
                        new DVDTitle()
                        {
                            DVDNumber = 1,
                            CategoryNumber = 4,
                            StudioNumber = 10,
                            ProducerNumber = 1,
                            DVDTitle = "Pumpkinhead",
                            DateReleased = DateTime.ParseExact("1989-01-13","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 2,
                            CategoryNumber = 10,
                            StudioNumber = 1,
                            ProducerNumber = 2,
                            DVDTitle = "Moto8",
                            DateReleased = DateTime.ParseExact("2020-03-05","yyyy-MM-dd",null),
                            StandardCharge = 150,
                            PenaltyCharge = 10,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 3,
                            CategoryNumber = 6,
                            StudioNumber = 2,
                            ProducerNumber = 3,
                            DVDTitle = "Avengers: Endgame",
                            DateReleased = DateTime.ParseExact("2019-04-26","yyyy-MM-dd",null),
                            StandardCharge = 300,
                            PenaltyCharge = 25,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 4,
                            CategoryNumber = 6,
                            StudioNumber = 7,
                            ProducerNumber = 4,
                            DVDTitle = "Star Wars: Episode IX",
                            DateReleased = DateTime.ParseExact("2019-12-20","yyyy-MM-dd",null),
                            StandardCharge = 250,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 5,
                            CategoryNumber = 3,
                            StudioNumber = 3,
                            ProducerNumber = 6,
                            DVDTitle = "Joker",
                            DateReleased = DateTime.ParseExact("2019-10-04","yyyy-MM-dd",null),
                            StandardCharge = 150,
                            PenaltyCharge = 10,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 6,
                            CategoryNumber = 3,
                            StudioNumber = 4,
                            ProducerNumber = 5,
                            DVDTitle = "Inception",
                            DateReleased = DateTime.ParseExact("2010-07-08","yyyy-MM-dd",null),
                            StandardCharge = 250,
                            PenaltyCharge = 20,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 7,
                            CategoryNumber = 9,
                            StudioNumber = 5,
                            ProducerNumber = 8,
                            DVDTitle = "John Wick",
                            DateReleased = DateTime.ParseExact("2014-10-24","yyyy-MM-dd",null),
                            StandardCharge = 150,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 8,
                            CategoryNumber = 6,
                            StudioNumber = 6,
                            ProducerNumber = 7,
                            DVDTitle = "Dune",
                            DateReleased = DateTime.ParseExact("2021-10-22","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 9,
                            CategoryNumber = 2,
                            StudioNumber = 8,
                            ProducerNumber = 1,
                            DVDTitle = "Kicks",
                            DateReleased = DateTime.ParseExact("2016-07-01","yyyy-MM-dd",null),
                            StandardCharge = 100,
                            PenaltyCharge = 5,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 10,
                            CategoryNumber = 7,
                            StudioNumber = 10,
                            ProducerNumber = 8,
                            DVDTitle = "Pleasure",
                            DateReleased = DateTime.ParseExact("2021-10-20","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 10,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 11,
                            CategoryNumber = 4,
                            StudioNumber = 9,
                            ProducerNumber = 1,
                            DVDTitle = "Dont Breathe",
                            DateReleased = DateTime.ParseExact("2016-03-12","yyyy-MM-dd",null),
                            StandardCharge = 100,
                            PenaltyCharge = 10,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 12,
                            CategoryNumber = 1,
                            StudioNumber = 1,
                            ProducerNumber = 9,
                            DVDTitle = "Palm Springs",
                            DateReleased = DateTime.ParseExact("2021-03-04","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 13,
                            CategoryNumber = 8,
                            StudioNumber = 6,
                            ProducerNumber = 10,
                            DVDTitle = "Black Widow",
                            DateReleased = DateTime.ParseExact("2021-07-09","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 20,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 14,
                            CategoryNumber = 2,
                            StudioNumber = 5,
                            ProducerNumber = 4,
                            DVDTitle = "The Shawshank Redemption",
                            DateReleased = DateTime.ParseExact("1994-09-22","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 20,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 15,
                            CategoryNumber = 3,
                            StudioNumber = 3,
                            ProducerNumber = 7,
                            DVDTitle = "Jurassic Park",
                            DateReleased = DateTime.ParseExact("1993-06-09","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 16,
                            CategoryNumber = 3,
                            StudioNumber = 2,
                            ProducerNumber = 6,
                            DVDTitle = "Backdraft",
                            DateReleased = DateTime.ParseExact("1991-05-24","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 15,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 17,
                            CategoryNumber = 4,
                            StudioNumber = 8,
                            ProducerNumber = 2,
                            DVDTitle = "IT Follows",
                            DateReleased = DateTime.ParseExact("2015-03-27","yyyy-MM-dd",null),
                            StandardCharge = 150,
                            PenaltyCharge = 5,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 18,
                            CategoryNumber = 5,
                            StudioNumber = 9,
                            ProducerNumber = 3,
                            DVDTitle = "Titanic",
                            DateReleased = DateTime.ParseExact("1997-12-17","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 10,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 19,
                            CategoryNumber = 8,
                            StudioNumber = 5,
                            ProducerNumber = 2,
                            DVDTitle = "The Batman",
                            DateReleased = DateTime.ParseExact("2022-03-04","yyyy-MM-dd",null),
                            StandardCharge = 300,
                            PenaltyCharge = 30,
                        },
                        new DVDTitle()
                        {
                            DVDNumber = 20,
                            CategoryNumber = 3,
                            StudioNumber = 10,
                            ProducerNumber = 10,
                            DVDTitle = "Fight Club",
                            DateReleased = DateTime.ParseExact("1999-09-11","yyyy-MM-dd",null),
                            StandardCharge = 250,
                            PenaltyCharge = 25,
                        }

                    });

                    context.SaveChanges();

                }

                //Actor
                if(!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            ActorNumber = 1,
                            ActorFirstName = "Keanu",
                            ActorSurName = "Reeves"

                        },
                        new Actor()
                        {
                            ActorNumber = 2,
                            ActorFirstName = "Scarlett",
                            ActorSurName = "Johansson"

                        },
                        new Actor()
                        {
                            ActorNumber = 3,
                            ActorFirstName = "Robert",
                            ActorSurName = "Downey Jr."

                        },
                        new Actor()
                        {
                            ActorNumber = 4,
                            ActorFirstName = "Brad",
                            ActorSurName = "Pitt"

                        },
                        new Actor()
                        {
                            ActorNumber = 5,
                            ActorFirstName = "Leonardo",
                            ActorSurName = "Di Caprio"

                        },
                        new Actor()
                        {
                            ActorNumber = 6,
                            ActorFirstName = "Chris",
                            ActorSurName = "Hemsworth"

                        },
                        new Actor()
                        {
                            ActorNumber = 7,
                            ActorFirstName = "Morgan",
                            ActorSurName = "Freeman"

                        },
                        new Actor()
                        {
                            ActorNumber = 8,
                            ActorFirstName = "Robert",
                            ActorSurName = "Pattinson"

                        },
                        new Actor()
                        {
                            ActorNumber = 9,
                            ActorFirstName = "Mark",
                            ActorSurName = "Hamil"

                        },
                        new Actor()
                        {
                            ActorNumber = 10,
                            ActorFirstName = "Lance",
                            ActorSurName = "Henriksen"

                        },
                        new Actor()
                        {
                            ActorNumber = 11,
                            ActorFirstName = "Haiden",
                            ActorSurName = "Deegan"

                        },
                        new Actor()
                        {
                            ActorNumber = 12,
                            ActorFirstName = "Cristin",
                            ActorSurName = "Milioti"

                        },
                        new Actor()
                        {
                            ActorNumber = 13,
                            ActorFirstName = "Andy",
                            ActorSurName = "Samberg"

                        },
                        new Actor()
                        {
                            ActorNumber = 14,
                            ActorFirstName = "Maika",
                            ActorSurName = "Monroe"

                        },
                        new Actor()
                        {
                            ActorNumber = 15,
                            ActorFirstName = "Laura",
                            ActorSurName = "Dern"

                        },
                        new Actor()
                        {
                            ActorNumber = 16,
                            ActorFirstName = "Jaoquin",
                            ActorSurName = "Phoenix"

                        },
                        new Actor()
                        {
                            ActorNumber = 17,
                            ActorFirstName = "Timothee",
                            ActorSurName = "Chalamet"

                        },
                        new Actor()
                        {
                            ActorNumber = 18,
                            ActorFirstName = "Sofia",
                            ActorSurName = "Kappel"

                        },
                        new Actor()
                        {
                            ActorNumber = 19,
                            ActorFirstName = "Stephen",
                            ActorSurName = "Lang"

                        },
                        new Actor()
                        {
                            ActorNumber = 20,
                            ActorFirstName = "William",
                            ActorSurName = "Baldwin"

                        },
                        new Actor()
                        {
                            ActorNumber = 21,
                            ActorFirstName = "Chris",
                            ActorSurName = "Hemsworth"

                        },
                        new Actor()
                        {
                            ActorNumber = 22,
                            ActorFirstName = "Jahking",
                            ActorSurName = "Guillory"

                        },
                        new Actor()
                        {
                            ActorNumber = 23,
                            ActorFirstName = "Jared",
                            ActorSurName = "Leto"

                        },
                        new Actor()
                        {
                            ActorNumber = 24,
                            ActorFirstName = "Tom",
                            ActorSurName = "Hardy"

                        },
                        new Actor()
                        {
                            ActorNumber = 25,
                            ActorFirstName = "Zoe",
                            ActorSurName = "Kravitz"

                        }
                    });

                    context.SaveChanges();

                }


                //CastMember
                if(!context.CastMembers.Any())
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

                        }
                    });

                    context.SaveChanges();
                }

                //DVDCopy
                if(!context.DVDCopies.Any())
                {
                    context.DVDCopies.AddRange(new List<DVDCopy>()
                    {
                        new DVDCopy()
                        {
                            CopyNumber = 1,
                            DVDNumber = 1,
                            DatePurchased = DateTime.ParseExact("2022-04-11","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 2,
                            DVDNumber = 2,
                            DatePurchased = DateTime.ParseExact("2022-04-10","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 3,
                            DVDNumber = 3,
                            DatePurchased = DateTime.ParseExact("2022-04-01","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 4,
                            DVDNumber = 4,
                            DatePurchased = DateTime.ParseExact("2022-04-12","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 5,
                            DVDNumber = 5,
                            DatePurchased = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 6,
                            DVDNumber = 6,
                            DatePurchased = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 7,
                            DVDNumber = 7,
                            DatePurchased = DateTime.ParseExact("2022-03-20","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 8,
                            DVDNumber = 8,
                            DatePurchased = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 9,
                            DVDNumber = 9,
                            DatePurchased = DateTime.ParseExact("2022-04-12","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 10,
                            DVDNumber = 10,
                            DatePurchased = DateTime.ParseExact("2022-04-01","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 11,
                            DVDNumber = 11,
                            DatePurchased = DateTime.ParseExact("2022-04-08","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 12,
                            DVDNumber = 12,
                            DatePurchased = DateTime.ParseExact("2022-04-25","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 13,
                            DVDNumber = 13,
                            DatePurchased = DateTime.ParseExact("2022-04-04","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 14,
                            DVDNumber = 14,
                            DatePurchased = DateTime.ParseExact("2022-04-06","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 15,
                            DVDNumber = 15,
                            DatePurchased = DateTime.ParseExact("2022-04-10","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 16,
                            DVDNumber = 16,
                            DatePurchased = DateTime.ParseExact("2022-03-29","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 17,
                            DVDNumber = 17,
                            DatePurchased = DateTime.ParseExact("2022-04-27","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 18,
                            DVDNumber = 18,
                            DatePurchased = DateTime.ParseExact("2022-04-21","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 19,
                            DVDNumber = 19,
                            DatePurchased = DateTime.ParseExact("2022-04-09","yyyy-MM-dd",null)
                        },
                        new DVDCopy()
                        {
                            CopyNumber = 20,
                            DVDNumber = 20,
                            DatePurchased = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null)
                        }


                    });

                    context.SaveChanges();

                }

                //MembershipCategory
                if(!context.MembershipCategories.Any())
                {
                    context.MembershipCategories.AddRange(new List<MembershipCategory>()
                    {
                        new MembershipCategory()
                        {
                            MembershipCategoryNumber = 1,
                            MembershipCategoryDescription = "Basic",
                            MembershipCategoryTotalLoans = 1

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryNumber = 2,
                            MembershipCategoryDescription = "Standard",
                            MembershipCategoryTotalLoans = 3

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryNumber = 3,
                            MembershipCategoryDescription = "Medium",
                            MembershipCategoryTotalLoans = 5

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryNumber = 4,
                            MembershipCategoryDescription = "Premium",
                            MembershipCategoryTotalLoans = 10

                        },
                        new MembershipCategory()
                        {
                            MembershipCategoryNumber = 5,
                            MembershipCategoryDescription = "Ultimate",
                            MembershipCategoryTotalLoans = 15

                        },

                    });

                    context.SaveChanges();

                }


                //Member
                if(!context.Members.Any())
                {
                    context.Members.AddRange(new List<Member>()
                    {
                        new Member()
                        {
                            MemberNumber = 1,
                            MembershipCategoryNumber = 4,
                            MemberFirstName = "Abhishek",
                            MemberLastName = "Rokaya",
                            MemberAddress = "Samakhusi, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2001-04-15","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 2,
                            MembershipCategoryNumber = 2,
                            MemberFirstName = "Ushaan",
                            MemberLastName = "Shrestha",
                            MemberAddress = "Jyatha, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2000-10-07","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 3,
                            MembershipCategoryNumber = 1,
                            MemberFirstName = "Oshriya",
                            MemberLastName = "Manandhar",
                            MemberAddress = "Budhanilkantha, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2001-06-25","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 4,
                            MembershipCategoryNumber = 5,
                            MemberFirstName = "Animesh",
                            MemberLastName = "Basnet",
                            MemberAddress = "Satdobato, Lalitpur",
                            MemberDateOfBirth = DateTime.ParseExact("1998-08-20","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 5,
                            MembershipCategoryNumber = 2,
                            MemberFirstName = "Pranamya",
                            MemberLastName = "Sharma",
                            MemberAddress = "Naikap, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2002-05-29","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 6,
                            MembershipCategoryNumber = 1,
                            MemberFirstName = "Prabhas",
                            MemberLastName = "Khanal",
                            MemberAddress = "Thimi, Bhaktapur",
                            MemberDateOfBirth = DateTime.ParseExact("1996-12-25","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 7,
                            MembershipCategoryNumber = 3,
                            MemberFirstName = "Isabella",
                            MemberLastName = "Gurung",
                            MemberAddress = "Patan, Lalitpur",
                            MemberDateOfBirth = DateTime.ParseExact("2006-10-05","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 8,
                            MembershipCategoryNumber = 2,
                            MemberFirstName = "Bharat",
                            MemberLastName = "Humagain",
                            MemberAddress = "Tangal, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("2008-11-29","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 9,
                            MembershipCategoryNumber = 5,
                            MemberFirstName = "Ishan",
                            MemberLastName = "Bajracharya",
                            MemberAddress = "Thamel, Kathmandu",
                            MemberDateOfBirth = DateTime.ParseExact("1997-08-28","yyyy-MM-dd",null)

                        },
                        new Member()
                        {
                            MemberNumber = 10,
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
                if(!context.LoanTypes.Any())
                {
                    context.LoanTypes.AddRange(new List<LoanType>()
                    {
                        new LoanType()
                        {
                            LoanTypeNumber = 1,
                            LoanTypeTitle = "Trebel Trouble",
                            LoanDuration = 3

                        },
                        new LoanType()
                        {
                            LoanTypeNumber = 2,
                            LoanTypeTitle = "Five Star",
                            LoanDuration = 5

                        },
                        new LoanType()
                        {
                            LoanTypeNumber = 3,
                            LoanTypeTitle = "Weekly",
                            LoanDuration = 7

                        },
                        new LoanType()
                        {
                            LoanTypeNumber = 4,
                            LoanTypeTitle = "New Moon",
                            LoanDuration = 15

                        },
                        new LoanType()
                        {
                            LoanTypeNumber = 5,
                            LoanTypeTitle = "Whole Month",
                            LoanDuration = 30

                        }

                    });

                    context.SaveChanges();

                }

                //Loan
                if(!context.Loans.Any())
                {
                    context.Loans.AddRange(new List<Loan>()
                    {
                        new Loan()
                        {
                            LoanNumber = 1,
                            LoanTypeNumber = 2,
                            CopyNumber = 3,
                            MemberNumber = 7,
                            DateOut = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-25","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-24","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 2,
                            LoanTypeNumber = 1,
                            CopyNumber = 13,
                            MemberNumber = 2,
                            DateOut = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-18","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-18","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 3,
                            LoanTypeNumber = 3,
                            CopyNumber = 16,
                            MemberNumber = 10,
                            DateOut = DateTime.ParseExact("2022-04-10","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-16","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 4,
                            LoanTypeNumber = 5,
                            CopyNumber = 1,
                            MemberNumber = 9,
                            DateOut = DateTime.ParseExact("2022-03-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-16","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 5,
                            LoanTypeNumber = 4,
                            CopyNumber = 8,
                            MemberNumber = 1,
                            DateOut = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-30","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 6,
                            LoanTypeNumber = 2,
                            CopyNumber = 12,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-04-15","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 7,
                            LoanTypeNumber = 1,
                            CopyNumber = 20,
                            MemberNumber = 8,
                            DateOut = DateTime.ParseExact("2022-04-27","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-30","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 8,
                            LoanTypeNumber = 5,
                            CopyNumber = 6,
                            MemberNumber = 5,
                            DateOut = DateTime.ParseExact("2022-03-17","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-17","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 9,
                            LoanTypeNumber = 3,
                            CopyNumber = 17,
                            MemberNumber = 6,
                            DateOut = DateTime.ParseExact("2022-04-21","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-28","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-26","yyyy-MM-dd",null)
                        },

                        new Loan()
                        {
                            LoanNumber = 10,
                            LoanTypeNumber = 1,
                            CopyNumber = 9,
                            MemberNumber = 4,
                            DateOut = DateTime.ParseExact("2022-04-29","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            DateReturned = DateTime.ParseExact("2022-04-30","yyyy-MM-dd",null)
                        }

                    });

                    context.SaveChanges();

                }
            
            }
        }
    }
}
