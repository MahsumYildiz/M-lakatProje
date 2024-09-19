using Microsoft.EntityFrameworkCore;
using MülakatProje.Context;

namespace MülakatProje
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure Entity Framework Core to use SQL Server
            builder.Services.AddDbContext<VeritabaniContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MusicDatabase") + ";TrustServerCertificate=True"));
     


            // Add Swagger and API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
