using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
	public class BookFormVM
	{
		public int Id { get; set; }
		[MaxLength(50)]
		public string Title { get; set; }
		
		[Display(Name ="Auther Name")]
		public int AutherId { get; set; }

		public List<SelectListItem>?Authers { get; set; }

        public string Publisher { get; set; } = null!;
		[Display(Name ="Publish Date")]
        public DateTime PublisherDate { get; set;}=DateTime.Now;

		[Display(Name ="please choose your file...")]
        public IFormFile? ImageURL { get; set; }
        public string Description { get; set; } = null!;

		public List<int> SelectedCategories { get; set;} = new List<int>();
		public List<SelectListItem>? Categories { get; set; }

        
    }
}
