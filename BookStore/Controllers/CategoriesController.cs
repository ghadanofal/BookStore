using BookStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext context;

		public CategoriesController(ApplicationDbContext context)
		{
			this.context = context;
		}
		public IActionResult Index()
		{
			var result = context.Categories.ToList();
			return View("Index",result);
		}


	}
}
