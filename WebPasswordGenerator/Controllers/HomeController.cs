using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebPasswordGenerator.Models;

namespace WebPasswordGenerator.Controllers
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
            ViewData.Add(new KeyValuePair<string, object>("FirstHalfOfPassword", Request.Cookies["firstHalfOfPassword"]));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public void RememberPassword(string cookieValue)
        {
            if (cookieValue == null)
                cookieValue = "";
            CookieOptions cookieOptions = new CookieOptions() { SameSite = SameSiteMode.Strict, Secure = true, Expires = DateTime.Now.AddDays(500) };
            Response.Cookies.Append("firstHalfOfPassword", cookieValue, cookieOptions);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
