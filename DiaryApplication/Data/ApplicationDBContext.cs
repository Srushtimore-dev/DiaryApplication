using DiaryApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace DiaryApplication.Data
{
	public class ApplicationDBContext : DbContext
	{
		internal readonly object DairyEntry;

		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}

		public DbSet<DiaryEntry> DiaryEntries{ get; set; }

		
	}
}

