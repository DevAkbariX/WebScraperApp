
using EFCore.AutomaticMigrations;
using Microsoft.EntityFrameworkCore;
using WebScraper.Application.Services;
using WebScraper.Domain.Interfaces;
using WebScraper.Infrastructure.Data;
using WebScraper.Infrastructure.Repositories;

namespace WebScraper.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            #region dbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });
            #endregion

            #region DI
            builder.Services.AddScoped<INewsRepository, NewsRepository>();
            builder.Services.AddScoped<NewsScraperService>();
            builder.Services.AddHostedService<BackgroundScraper>();
            #endregion

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #region AutoMigration

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var dbcontext = service.GetRequiredService<AppDbContext>();
            var changes = dbcontext.ChangeTracker.Entries().ToList();
            await dbcontext.MigrateToLatestVersionAsync();
            #endregion

            app.Run();
        }
    }
}