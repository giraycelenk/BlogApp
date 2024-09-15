using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestDataFill(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text = "Web Programlama" },
                        new Tag { Text = "Backend" },
                        new Tag { Text = "Frontend" },
                        new Tag { Text = "FullStack" },
                        new Tag { Text = "PHP" }
                    );
                    context.SaveChanges();
                }
            }

            if(!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "giraycelenk" },
                    new User { UserName = "testkullanici" }
                );
                context.SaveChanges();
            }
            
            if(!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post { 
                            Title = "ASP.NET Core",
                            Content = "Asp.net core content",
                            IsActive = true,
                            PublishedOn = DateTime.Now,
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1 
                        },
                        new Post { 
                            Title = "PHP",
                            Content = "PHP content",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            UserId = 1 
                        },
                        new Post { 
                            Title = "Django",
                            Content = "Django content",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2 
                        }
                );
                context.SaveChanges();
            }

        }
    }
}