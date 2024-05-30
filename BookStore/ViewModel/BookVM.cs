using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        
        public string Auther { get; set; }

        public List<SelectListItem>? Authers { get; set; }

        public string Publisher { get; set; } = null!;
        
        public DateTime PublisherDate { get; set; } 

        public String ImageURL { get; set; }
        public string Description { get; set; } = null!;

        public List<string> Categories { get; set; } 
        
    }
}
