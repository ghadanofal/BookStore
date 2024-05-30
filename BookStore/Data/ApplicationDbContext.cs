using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Auther> Authers { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories {get;set;}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<BookCategory>().HasKey(e => new
			{
				e.CategoryId,
				e.BookId
			});
			base.OnModelCreating(builder);
		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		
	}
}
