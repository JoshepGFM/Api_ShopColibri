using Microsoft.Data.SqlClient;
using API_ShopColibri.Models;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Attributes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Timers;

internal class Program
{
    private static string[] Scopes = { DriveService.Scope.Drive };
    private static string AplicationName = "ShopColibriApp";
    private static System.Timers.Timer _timer;
    private static Drive drive = new Drive();

    private static void Main(string[] args)
    {
        // Crear el temporizador con el intervalo calculado para refrescar el token
        _timer = new System.Timers.Timer(60 * 60 * 1000);
        _timer.Elapsed += TimerElapsed;
        _timer.Start();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

        string conn = CnnStrBuilder.ConnectionString;

        builder.Services.AddDbContext<ShopColibriContext>(options => options.UseSqlServer(conn));

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
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        drive.RefreshToken();
    }
}