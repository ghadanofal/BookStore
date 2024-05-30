using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public class AuthersController : Controller
	{
		private readonly ApplicationDbContext context;

		public  AuthersController (ApplicationDbContext context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			var authers = context.Authers.ToList();
			var authersVM = new List<AutherVM>();
			
			foreach(var auther in authers)
			{
				var autherVM = new AutherVM()
				{
					Id = auther.Id,
					Name = auther.Name,
					CreatedOn = auther.CreatedOn,
					UpdatedOn = auther.UpdatedOn
				};
				authersVM.Add(autherVM);
				
			}
			return View(authersVM);
		}
		
		[HttpGet]
		public IActionResult Create()
		{

			return View("Form");
		}
		[HttpPost]
        public IActionResult Create(AutherFormVM autherForm)
        {
			if(!ModelState.IsValid)
			{
				return View("Form", autherForm);
            }

			var auther = new Auther
			{
				Name = autherForm.Name
			};
			context.Authers.Add(auther);
			context.SaveChanges();
			return RedirectToAction("Index");
        }
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var auther = context.Authers.Find(id);

			if (auther == null)
			{
				return NotFound();
			}

			var viewModel = new AutherFormVM
			{
				Id = id,
				Name = auther.Name,
			};

			return View("Form", viewModel);
		}

		[HttpPost]
        public IActionResult Edit(AutherFormVM autherForm)
        {
			if (!ModelState.IsValid)
			{
                return View("Form", autherForm);
            }
            var auther = context.Authers.Find(autherForm.Id);

            if (auther == null)
            {
                return NotFound();
            }

			auther.Name = autherForm.Name;
			context.SaveChanges();
			return RedirectToAction("Index");

        }

    }
}
