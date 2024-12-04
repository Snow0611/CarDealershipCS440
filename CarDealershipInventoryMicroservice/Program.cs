using Microsoft.EntityFrameworkCore;
using CarDealership.Models;

// Setting up configuration for the web api
var builder = WebApplication.CreateBuilder(args);

// Adding package references to the model classes
builder.Services.AddScoped<CarFunctions>();

// Adding controller and view support
builder.Services.AddControllersWithViews();

// Adding HTTP client for conneting to api
builder.Services.AddHttpClient("ApiClient", httpClient => { httpClient.BaseAddress = new Uri("https://localhost:7179"); });

// Initializing DatabaseContext object with 'option' paramaters, connecting to the SQL server
// using connection string from appsettings.json, then adding DatabaseContext to the web api
builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"), option => option.EnableRetryOnFailure()));

// Building the web api
var app = builder.Build();

// Defining the routing rules and creating
// a default route to the index page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=index}/{action=Index}/{id?}"
    );

// Running the application
app.Run();
