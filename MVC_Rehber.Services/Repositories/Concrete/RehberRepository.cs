using Microsoft.EntityFrameworkCore;
using MVC_Rehber.DataAcces.Context;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.Repositories.Abstract;

namespace MVC_Rehber.Services.Repositories.Concrete
{
	public class RehberRepository : IRehberRepository
	{
		private readonly ApplicationDbContext _context;

		public RehberRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IQueryable<Rehber> Rehber => _context.Rehber;
		public async Task<Rehber> GetByIdAsync(int id) => await _context.Rehber.FindAsync(id);
		public async Task<List<Uye>> UyeListesiAsync() => await _context.Uye.ToListAsync();
		public async Task<bool> RehberEkleAsync(Rehber rehber)
		{
			await _context.Rehber.AddAsync(rehber);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> RehberGuncelleAsync(Rehber rehber)
		{
			await Task.Run(() =>
			{
				_context.Rehber.Update(rehber);
			});
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<List<Rehber>> RehberUyeDahilEtAsync() => await _context.Rehber.Include(x => x.Uyeler).ToListAsync();
		public async Task<bool> RehberSilAsync(int id)
		{
			_context.Rehber.Remove(await GetByIdAsync(id));
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
