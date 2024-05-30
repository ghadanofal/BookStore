using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
	public class AutherVM
	{
		public int Id { get; set; }
		
		public string Name { get; set; } = null!;
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public DateTime UpdatedOn { get; set; }
	}
}
