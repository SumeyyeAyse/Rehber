using Microsoft.AspNetCore.Identity;
using MVC_Rehber.DataAcces.Context;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Rehber.Services.Repositories.Concrete
{
	public class UyeRepository : IUyeRepository
	{
		private readonly ApplicationDbContext _context;
		public UyeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IQueryable<Uye> Uye => _context.Uye;
		public IQueryable<IdentityRole> Roles => _context.Roles;
		public async Task<Uye> GetByIdAsync(int id)
		{
			return await _context.Uye.FindAsync(id);
		}
		public async Task<bool> UyeEkleAsync(Uye uye)
		{
			await _context.Uye.AddAsync(uye);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<bool> UyeGuncelleAsync(Uye uye)
		{
			await Task.Run(() =>
			{
				_context.Uye.Update(uye);
			});
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> UyeKaydetAsync() => await _context.SaveChangesAsync() > 0;
		public async Task<bool> UyeSilAsync(int id)
		{
			_context.Uye.Remove(await GetByIdAsync(id));
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> AddRole(IdentityRole role)
		{
			await _context.Roles.AddAsync(role);
			return await _context.SaveChangesAsync() > 0;
		}

    }
}
