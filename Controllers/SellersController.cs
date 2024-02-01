using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerServices _sellerServices;
        private readonly DepartmentsServices _departmentsServices;

        public SellersController(SellerServices sellerServices, DepartmentsServices departmentsServices)
        {
            _sellerServices = sellerServices;
            _departmentsServices = departmentsServices;
        }

        public IActionResult Index()
        {

            var list = _sellerServices.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {

            var departments = _departmentsServices.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerServices.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}