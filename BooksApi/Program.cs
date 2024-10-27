
using BooksServiceApi.dbContext;
using BooksServiceApi.Interfaces;
using BooksServiceApi.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

namespace BooksApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IReaderService,ReaderService>();
            builder.Services.AddScoped<IBookExemplarService, BookExemplarService>();
            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped<IRentService,RentService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            builder.Services.AddDbContext<BooksApiDb>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
