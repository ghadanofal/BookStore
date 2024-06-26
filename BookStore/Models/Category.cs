﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
	public class Category
	{
		public int Id { get; set; }
		[MaxLength(50)]
		public string Name { get; set; } = null!;
		 public DateTime CreatedOn { get; set; }= DateTime.Now;
		public DateTime UpdatedOn { get; set;}

		public List<BookCategory> Books { get; set; } = new List<BookCategory>();
	}
}
