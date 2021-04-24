using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Models;
using Web.Helpers;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var clientSecret = "CleanArchitecture";
            var client = clientSecret.ToBase64EncodeWithKey("cGVydGFtaW5hcGVydGFtaW5hMTIxZHNhUzIubXF3cWFzYXcxNjIxNzJ5YWQ3NndxdGV5Z3lzbWNpdyohKkAhOTloYWhzYTAxOTIxOTc3NSkoISkqQCojJiUqQCUhJipHRElVQU9JVVMhXiVAKiYhSSEoKkApIUBfIStfK1M121asdsdass1221SASA121sa=");


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
