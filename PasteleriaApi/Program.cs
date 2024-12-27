
using Interfaces;
using Repositories;
using Services;

namespace PasteleriaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IPastelRepository, PastelRepository>();
            builder.Services.AddScoped<PasteleriaService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();

            // Redirigir automáticamente a Swagger
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger/index.html");
                return Task.CompletedTask;
            });

            app.MapControllers();

            app.Run();
        }
    }

}
