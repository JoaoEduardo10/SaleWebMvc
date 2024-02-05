using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordServices _salesRecordServices;

        public SalesRecordsController(SalesRecordServices salesRecordServices)
        {
            _salesRecordServices = salesRecordServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? min_date, DateTime? max_date)
        {

            if (!min_date.HasValue)
            {
                min_date = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!max_date.HasValue)
            {
                max_date = DateTime.Now;
            }

            ViewData["MinDate"] = min_date.Value.ToString("yyyy-MM-dd");
            ViewData["MaxDate"] = max_date.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordServices.FindByDateAsync(min_date, max_date);

            return View(result);
        }


        public IActionResult GroupingSearch()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}