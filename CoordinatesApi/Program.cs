using CoordinatesApi.Interfaces.Repositories;
using CoordinatesApi.Interfaces.Services;
using CoordinatesApi.Repositories;
using CoordinatesApi.Services;

namespace CoordinatesApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddScoped<ICoordinatesRepository, RandomCoordinatesRepository>();
        builder.Services.AddScoped<ICoordinatesService, CoordinatesService>();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}
