using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>{options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]);});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Users/Login";
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.TestDataFill(app);

app.MapControllerRoute(
    name: "post_details",
    pattern:"posts/details/{url}",
    defaults:new{controller = "Posts", action="Details"}
);

app.MapControllerRoute(
    name: "post_details",
    pattern:"posts/edit/{url}",
    defaults:new{controller = "Posts", action="Edit"}
);

app.MapControllerRoute(
    name: "post_details",
    pattern:"posts/delete/{url}",
    defaults:new{controller = "Posts", action="Delete"}
);

app.MapControllerRoute(
    name: "user_profile",
    pattern:"profile/{username}",
    defaults:new{controller = "Users", action="Profile"}
);

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern:"posts/tag/{tag}",
    defaults:new{controller = "Posts", action="Index"}
);

app.MapControllerRoute(
    name: "posts_pages",
    pattern:"posts/page/{page_num}",
    defaults:new{controller = "Posts", action="Index"}
);

app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Index}/{id?}"
);
app.Run();
