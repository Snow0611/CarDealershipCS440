using Microsoft.EntityFrameworkCore;
using CarDealership.CarDealership;

// Setting up configuration for the web api
var builder = WebApplication.CreateBuilder(args);

// Adding package references to the model classes
builder.Services.AddScoped<CarDealershipSystem>();

// Adding controller and view support
builder.Services.AddControllersWithViews();

// Initializing DatabaseContext object with 'option' paramaters, connecting to the SQL server
// using connection string from appsettings.json, then adding DatabaseContext to the web api
builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"), option => option.EnableRetryOnFailure()));

// Building the web api
var app = builder.Build();

// Defining the routing rules and creating
// a default route to the index page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CarDealershipSystem}/{action=Index}/{id?}"
    );

// Running the application
app.Run();
