using System;
using BoxOffice.Domain.Entities;
using BoxOffice.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace BoxOffice.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleShowDataAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Shows.Any())
            {
                var user = await userManager.FindByEmailAsync("administrator@localhost");
                context.Shows.AddRange(
                    new Show
                    {
                        Title = "Show 1",
                        Sessions =
                        {
                            new Session
                            {
                                Time = new DateTime(2020, 5, 15, 12, 0, 0),
                                PlacesLimit = 50,
                                Orders = 
                                {
                                    new Order
                                    {
                                        UserId = user.Id,
                                        Tickets =
                                        {
                                            new Ticket(),
                                            new Ticket(),
                                            new Ticket(),
                                            new Ticket(),
                                            new Ticket(),
                                        }
                                    }
                                }
                            },
                            new Session
                            {
                                Time = new DateTime(2020, 5, 20, 12, 0, 0),
                                PlacesLimit = 87
                            },
                            new Session
                            {
                                Time = new DateTime(2020, 5, 21, 19, 0, 0),
                                PlacesLimit = 56
                            }
                        }
                    },
                    new Show
                    {
                        Title = "Show 2",
                        Sessions =
                        {
                            new Session
                            {
                                Time = new DateTime(2020, 6, 15, 12, 0, 0),
                                PlacesLimit = 50
                            },
                            new Session
                            {
                                Time = new DateTime(2020, 6, 20, 12, 0, 0),
                                PlacesLimit = 90
                            },
                            new Session
                            {
                                Time = new DateTime(2020, 9, 21, 19, 0, 0),
                                PlacesLimit = 50
                            }
                        }
                    }
                );
                
                await context.SaveChangesAsync();
            }
        }
    }
}
