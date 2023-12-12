using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using MVC_Rehber.DataAcces.Context;
using MVC_Rehber.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Rehber.DataAcces.SeedMethods
{
	public class SeedData
	{
		private string CreatePasswordHash(IdentityUser user, string password)
		{
			var passwordHasher = new PasswordHasher<IdentityUser>();
			return passwordHasher.HashPassword(user, password);
		}
		public static void Seed(IApplicationBuilder app)
		{
			using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
			{
				ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				context.Database.Migrate();

				if(!context.Users.Any(x => x.UserName == "admin@gmail.com"))
				{
					IdentityUser adminUser = new IdentityUser
					{
						UserName = "admin@gmail.com",
						NormalizedUserName = "ADMIN@GMAIL.COM",
						Email = "admin@gmail.com",
						NormalizedEmail = "ADMIN@GMAIL.COM",
						PhoneNumber = null,
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						SecurityStamp = Guid.NewGuid().ToString(),
					};
					var passwordHashConverter = new SeedData();
					adminUser.PasswordHash = passwordHashConverter.CreatePasswordHash(adminUser, "admin");
					context.Users.AddRange(adminUser);
				}
				context.SaveChanges();


				if(!context.Roles.Any(x => x.Name == "Admin"))
				{
					IdentityRole role = new IdentityRole
					{
						Name = "Admin",
						NormalizedName = "ADMIN",
						ConcurrencyStamp = Guid.NewGuid().ToString(),
					};
					context.Roles.AddRange(role);
				}
				context.SaveChanges();

				if(!context.Roles.Any(x => x.Name == "Admin"))
				{
					context.UserRoles.AddRange(new IdentityUserRole<string>()
					{
						UserId = context.Users.FirstOrDefault(x => x.UserName == "admin@gmail.com").Id,
						RoleId = context.Roles.FirstOrDefault(x => x.Name == "Admin").Id
					});
				}
				context.SaveChanges();

				if(!context.Uye.Any(x => x.Ad == "Admin"))
				{
					context.Uye.AddRange(
						new Uye
						{
							Ad = "Admin",
							Soyad = "Admin",
							KullaniciAdi = "Admin",
						});
				}
				context.SaveChanges();


			}
		}
	}
}
