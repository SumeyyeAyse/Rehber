using Microsoft.AspNetCore.Identity;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Rehber.Services.Repositories.Abstract
{
	public interface IUyeRepository
	{
		IQueryable<Uye> Uye { get; }
		IQueryable<IdentityRole> Roles { get; }
		Task<Uye> GetByIdAsync(int id);
		Task<bool> UyeEkleAsync(Uye uye);
		Task<bool> UyeGuncelleAsync(Uye uye);
		Task<bool> UyeSilAsync(int id);
		Task<bool> UyeKaydetAsync();
		Task<bool> AddRole(IdentityRole role);
	}
}
