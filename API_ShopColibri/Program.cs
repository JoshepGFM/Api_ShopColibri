using Microsoft.Data.SqlClient;
using API_ShopColibri.Models;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Attributes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

internal class Program
{
    private static string[] Scopes = { DriveService.Scope.Drive };
    private static string AplicationName = "ShopColibriApp";

    private static void Main(string[] args)
    {
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

    private bool VerificarAcceso()
    {
        bool R = false;
        UserCredential credential;

        //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Drive)).Assembly;
        //Stream Json = assembly.GetManifestResourceStream("ShopColibriApp.CredenDri.json");
        //string StreamReader = new StreamReader(Json).ReadToEnd();

        using (var stream = new FileStream("CredenDri.json", FileMode.Open, FileAccess.Read))
        {
            string creadPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(creadPath, true)).Result;
            Console.WriteLine("Credential file saved to: " + creadPath);
        }

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = AplicationName,
        });

        FilesResource.ListRequest listRequest = service.Files.List();
        listRequest.PageSize = 10;
        listRequest.Fields = "nextPageToken, files(id, name)";

        IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
        Console.WriteLine("Files:");
        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                Console.WriteLine("{0} ({1})", file.Name, file.Id);
            }
        }
        else
        {
            Console.WriteLine("No files found.");
        }
        Console.Read();
        return R;
    }
}