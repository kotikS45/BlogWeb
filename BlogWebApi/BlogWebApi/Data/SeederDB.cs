using BlogWebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<AppEFContext>();
                context.Database.Migrate();

                if (!context.Categories.Any())
                {
                    var categories = new List<CategoryEntity>
                    {
                        new CategoryEntity
                        {
                            Name = "Technology",
                            UrlSlug = "technology",
                            Description = "Articles about technology",
                            DateCreated = DateTime.UtcNow
                        },
                        new CategoryEntity { 
                            Name = "Science", 
                            UrlSlug = "science", 
                            Description = "Articles about science",
                            DateCreated = DateTime.UtcNow
                        }
                    };
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }
                if (!context.Tags.Any())
                {
                    var tags = new List<TagEntity>
                    {
                        new TagEntity {
                            Name = "C#",
                            UrlSlug = "c-sharp",
                            Description = "Articles about C# programming language",
                            DateCreated = DateTime.UtcNow
                        },
                        new TagEntity {
                            Name = "ASP.NET",
                            UrlSlug = "asp-net",
                            Description = "Articles about ASP.NET framework",
                            DateCreated = DateTime.UtcNow
                        }
                    };
                    context.Tags.AddRange(tags);
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    var posts = new List<PostEntity>
                    {
                        new PostEntity {
                            Id = 1,
                            Title = "Introduction to Entity Framework",
                            ShortDescription = "Learn the basics of Entity Framework",
                            Description = "Entity Framework is an ORM framework...",
                            Meta = "Entity Framework, ORM",
                            UrlSlug = "introduction-to-ef",
                            Published = true,
                            PostedOn = DateTime.UtcNow,
                            DateCreated = DateTime.UtcNow,
                            Category = context.Categories.First()
                        }
                    };
                    context.Posts.AddRange(posts);
                    context.PostTags.AddRange(new PostTagEntity { PostId = 1, TagId = 1 }, new PostTagEntity { PostId = 1, TagId = 2 });
                    context.SaveChanges();
                }
            }
        }
    }
}
