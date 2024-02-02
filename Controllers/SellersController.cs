using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.services;
using SalesWebMvc.services.Exceptions;

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

        public async Task<IActionResult> Index()
        {

            var list = await _sellerServices.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {

            var departments = await _departmentsServices.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var departments = await _departmentsServices.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            await _sellerServices.InsertAsync(seller);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found" });
            }

            var seller = await _sellerServices.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "vedendor não encontrado" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerServices.RemoveAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found" });
            }

            var seller = await _sellerServices.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vedendor não encontrado" });
            }

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found" });
            }

            var seller = await _sellerServices.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vedendor não encontrado" });
            }

            if (!ModelState.IsValid)
            {
                var department = await _departmentsServices.FindAllAsync();
                var formViewModel = new SellerFormViewModel { Departments = department, Seller = seller };
                return View(formViewModel);
            }

            var departments = await _departmentsServices.FindAllAsync();
            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "usuárionão encontrado" });
            }

            try
            {
                await _sellerServices.UpdateAsync(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {

            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}