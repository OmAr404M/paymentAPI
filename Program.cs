
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using paymentAPI.Models;

namespace paymentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PaymentDetailContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(Options =>
            Options.WithOrigins("http://localhost:64746")
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
