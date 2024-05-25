using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BookStore.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext context;
		private object categoryVM;

		public CategoriesController(ApplicationDbContext context)
		{
			this.context = context;
		}
		public IActionResult Index()
		{
			var result = context.Categories.ToList();
			return View("Index",result);
		}

		[HttpGet]
        public IActionResult Create()
        {
            
            return View("Create");
        }

		[HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
			if (!ModelState.IsValid)
			{
				return View("Create", categoryVM);
			}

            var category = new Category
			{
				
				Name = categoryVM.Name
			};
            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var category = context.Categories.Find(id);
				
			if(category == null)
			{
				return NotFound();
			}
			var viewModel = new CategoryVM
			{
				Id = id,
				Name = category.Name
			};
			return View("Create", viewModel);
		}
		[HttpPost]
		public IActionResult Edit(CategoryVM categoryVM)
		{
			if (!ModelState.IsValid)
			{
				return View("create", categoryVM);
			}
			var category = context.Categories.Find(categoryVM.Id);
			if(category == null)
			{
				return NotFound();
			}

			category.Name = categoryVM.Name;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Details(int id)
		{
			var category = context.Categories.Find(id);
			if(category == null)
			{
				return NotFound();
			}

			var viewModel = new CategoryVM
			{
				Id = category.Id,
				Name = category.Name,
				CreatedOn = category.CreatedOn,
				UpdatedOn = category.UpdatedOn
			};
			return View("Detials", viewModel);
		}
		public IActionResult Delete(CategoryVM categoryVM)
		{

			var category = context.Categories.Find(categoryVM.Id);
			if( category == null )
			{
				return NotFound();
			}
			context.Categories.Remove(category);
			context.SaveChanges();
			return RedirectToAction("Index");

			
		}
    }
}
