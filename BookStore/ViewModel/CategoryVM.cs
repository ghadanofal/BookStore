using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
	public class CategoryVM
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="plz insert name")]
		[MaxLength(35, ErrorMessage ="name should be 35 charachter")]
		public string Name { get; set; } = null!;

	}
}
