using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zhankui_Wang_Prob_Asst_3_Part_1.Models;
using Zhankui_Wang_Prob_Asst_3_Part_1.Utilities;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Controllers
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

        public IActionResult TestFirstMonday()
        {
            int year = 2024;
            int month = 8;
            DateTime result = Utility.FirstMondayOfSecondWeek(year, month);

            return Content($"The first Monday of the second week in {year}/{month} is: {result.ToShortDateString()}");
        }
    }
}
