using GqlNetApi.Data;
using GqlNetApi.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GqlNetApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            using IServiceScope scope = host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var authorDbEntry = ctx.Authors.Add(
                new Author
                {
                    Name = "Tolkien"
                });

            ctx.SaveChanges();

            ctx.Books.AddRange(
                new Book
                {
                    Name = "First Book",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id.ToString(),
                    Author = authorDbEntry.Entity,
                    Genre = "Fantasy"
                },
                new Book
                {
                    Name = "First Book",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id.ToString(),
                    Author = authorDbEntry.Entity,
                    Genre = "Mystery"
                },
                new Book
                {
                    Name = "Second Book",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id.ToString(),
                    Author = authorDbEntry.Entity,
                    Genre = "Crime"
                }
            );

            ctx.SaveChanges();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
