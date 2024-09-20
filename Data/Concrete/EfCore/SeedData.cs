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
                        new Tag { Text = "Web Programlama", Url = "web-programlama", Color=TagColors.info},
                        new Tag { Text = "Backend" ,Url = "backend",Color=TagColors.primary},
                        new Tag { Text = "Frontend" ,Url = "frontend",Color=TagColors.danger},
                        new Tag { Text = "FullStack" ,Url = "fullstack",Color=TagColors.warning},
                        new Tag { Text = "PHP" ,Url = "php",Color=TagColors.success}
                    );
                    context.SaveChanges();
                }
                
                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "giraycelenk",Name = "Giray Çelenk",Email="a.giraycelenk@hotmail.com",Password="123456",Image= "p1.jpg" },
                        new User { UserName = "testkullanici",Name = "Test Kullanıcı",Email="testkullanici@hotmail.com",Password="123456",Image= "p2.jpg" }
                    );
                    context.SaveChanges();
                }

                if(!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post { 
                                Title = "ASP.NET Core",
                                Content = "Asp.net core content",
                                Url = "aspnet-core",
                                IsActive = true,
                                PublishedOn = DateTime.Now,
                                Tags = context.Tags.Take(3).ToList(),
                                Image = "1.jpg",
                                UserId = 1,
                                Comments = new List<Comment>{
                                    new Comment {Text = "ASP.NET Core Comment 1", PublishedOn = DateTime.Now.AddDays(-20), UserId = 1},
                                    new Comment {Text = "ASP.NET Core Comment 2", PublishedOn = DateTime.Now.AddDays(-10), UserId = 2}
                                }
                            },
                            new Post { 
                                Title = "PHP",
                                Content = "PHP content",
                                Url = "php",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-20),
                                Tags = context.Tags.Take(2).ToList(),
                                Image = "2.jpg",
                                UserId = 1 
                            },
                            new Post { 
                                Title = "Django",
                                Content = "Django content",
                                Url = "django",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-30),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = 2 
                            },
                            new Post { 
                                Title = "React",
                                Content = "React content",
                                Url = "react",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-40),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = 2 
                            },
                            new Post { 
                                Title = "Angular",
                                Content = "Angular content",
                                Url = "angular",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-50),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = 2 
                            }
                            ,
                            new Post { 
                                Title = "Web Tasarım",
                                Content = "Web Tasarım content",
                                Url = "web-tasarim",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-60),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = 2 
                            }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}