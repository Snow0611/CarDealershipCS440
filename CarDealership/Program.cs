using Microsoft.EntityFrameworkCore;
using CarDealershipAPI.Models;

// Setting up configuration for the web api
var builder = WebApplication.CreateBuilder(args);

// Adding controller and view support
builder.Services.AddControllersWithViews();

// Creating HTTP clients to connect to the Car, Staff and Lot Microservices
builder.Services.AddHttpClient("CarClient", httpClient => { httpClient.BaseAddress = new Uri("https://localhost:7180"); });
builder.Services.AddHttpClient("StaffClient", httpClient => { httpClient.BaseAddress = new Uri("https://localhost:7181"); });
builder.Services.AddHttpClient("LotClient", httpClient => { httpClient.BaseAddress = new Uri("https://localhost:7182"); });

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
