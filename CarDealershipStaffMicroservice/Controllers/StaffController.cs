using CarDealershipStaff.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace CarDealershipStaff.Controllers
{
    public class StaffController : Controller
    {
        
        // Defining StaffFunctions and IHttpClientFactory objects
        private readonly StaffFunctions _staffFunctions;
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor for Controller
        // Initializes StaffFunctions and IHttpClientFactory objects,
        // to allow use of their functions
        public StaffController(StaffFunctions staffFunctions, IHttpClientFactory httpClientFactory)
        {
            _staffFunctions = staffFunctions;
            _httpClientFactory = httpClientFactory;
        }

        // Gets staff from database by StaffID field using getStaff() function,
        // then serializes staff as Json object and converts it into StringContent,
        // then posts StringContent back to api
        public async Task SalesReport(HttpClient httpClient, int staffID)
        {

            var staff = _staffFunctions.getStaff(staffID);

            var stringStaff = new StringContent(JsonSerializer.Serialize(staff));

            await httpClient.PostAsync("https://localhost:7179/Staff/SalesReport", stringStaff);
        }
    }
}
