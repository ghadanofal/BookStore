using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
	public class BooksController : Controller
	{
		private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public  BooksController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

		public IActionResult Index()
		{
			//because relation
			var books = context.Books
				.Include(book=>book.Auther)
				.Include(book=>book.Categories)
				.ThenInclude(book=>book.Category)
				.ToList();


			var booksVM = books.Select(item => new BookVM
			{
				Id = item.Id,
				Title = item.Title,
				Auther = item.Auther.Name,
				Description = item.Description,
				Publisher = item.Publisher,
				ImageURL = item.ImageURL,
				Categories = item.Categories.Select(item2=>item2.Category.Name).ToList(),
			}).ToList();
			/*var booksVM = new List<BookVM>();

			foreach(var item in books)
			{
				var bookVM = new BookVM
				{
					
				};
			foreach(var c in item.Categories)
				{
					bookVM.Categories.Add(c.Category.Name);
				}
				booksVM.Add(bookVM);

            }*/
			
            return View(booksVM);
		}
		[HttpGet]
		public IActionResult Create()
		{
			//convert from list to select list
			var authers = context.Authers.OrderBy(e=>e.Name).ToList();
			var categories = context.Categories.OrderBy(e=>e.Name).ToList();


            var authersList = new List<SelectListItem>();

			foreach(var auther in authers)
			{
				authersList.Add(new SelectListItem
				{
					Value = auther.Id.ToString(),
					Text = auther.Name
				});
			}


            var categoriesList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoriesList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            //convet to viewModel
            var viewModel = new BookFormVM
			{
				Authers = authersList,
				Categories = categoriesList
			};
			return View("Form", viewModel);
		}

		[HttpPost]
		public IActionResult Create(BookFormVM BookFormvm)
		{
			if (!ModelState.IsValid)
			{
				return View("Form", BookFormvm);
			}
			var imageName = "";

            if (BookFormvm.ImageURL != null)
			{
				imageName = Path.GetFileName(BookFormvm.ImageURL.FileName);
				var path = Path.Combine( $"{ webHostEnvironment.WebRootPath}/img/books", imageName);
				var stream = System.IO.File.Create(path);
				BookFormvm.ImageURL.CopyTo(stream);

                
			}

			var Book = new Book
			{
				Id = BookFormvm.Id,
				Title = BookFormvm.Title,
				Description = BookFormvm.Description,
				AutherId = BookFormvm.AutherId,
				Publisher = BookFormvm.Publisher,
				PublishDate = BookFormvm.PublisherDate,
				ImageURL = imageName,
				Categories = BookFormvm.SelectedCategories.Select(id => new BookCategory
				{
					CategoryId = id,
				}).ToList(),
            };

			context.Books.Add(Book);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(BookVM bookVM)
		{
			var book = context.Books.Find(bookVM.Id);
			if(book == null)
			{
				return NotFound();
			}
			var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/books", book.ImageURL);
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}
			
			context.Books.Remove(book);
			context.SaveChanges();
			return RedirectToAction("Index");
			//return Ok();
		}

	}
}
