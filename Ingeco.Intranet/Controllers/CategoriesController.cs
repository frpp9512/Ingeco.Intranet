using Ingeco.Intranet.Data.Interfaces;
using Ingeco.Intranet.Data.Models;
using Ingeco.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using SmartB1t.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IPostsManagementRepository _repository;

        public CategoriesController(IPostsManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var categories = await _repository.GetCategoriesAsync();
            var categoriesVM = categories.Select(c => 
            {
                var vm = c.GetViewModel();
                vm.PostsCount = _repository.GetCategoryPostCount(vm.Id)
                                           .GetAwaiter()
                                           .GetResult();
                return vm;
            });
            var vm = new CategoriesIndexViewModel { Categories = categoriesVM };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = viewModel.GetModel();
                await _repository.CreateCategory(model);
                TempData.SetModelCreated<CategoryViewModel, Guid>(model.Id);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var category = await _repository.GetCategoryAsync(id);
            var vm = category.GetViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.ExistCategory(viewModel.Id))
                {
                    var model = viewModel.GetModel();
                    await _repository.UpdateCategory(model);
                    TempData.SetModelUpdated<CategoryViewModel, Guid>(model.Id);
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var category = await _repository.GetCategoryAsync(id);
            if (category is null)
            {
                return BadRequest($"No existe la categoría con id: {id}");
            }
            await _repository.DeleteCategory(category);
            return Ok($"Se ha eliminado satisfactoriamente la categoría: {category.Name}");
        }
    }
}