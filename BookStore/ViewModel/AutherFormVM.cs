using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
	public class AutherFormVM
	{
		public int Id { get; set; }
		[MaxLength(50, ErrorMessage = "name length can't exceed 50")]
		public string Name { get; set; } = null!;
	}
}
