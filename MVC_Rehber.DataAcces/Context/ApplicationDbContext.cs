using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Rehber.Entities;

namespace MVC_Rehber.DataAcces.Context
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Uye> Uye { get; set; }
		public DbSet<Rehber> Rehber { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;database=RehberDB;Integrated Security=True;", b => b.MigrationsAssembly("MVC_Rehber.WebUI"));
            base.OnConfiguring(optionsBuilder);
        }
    }
   
}
