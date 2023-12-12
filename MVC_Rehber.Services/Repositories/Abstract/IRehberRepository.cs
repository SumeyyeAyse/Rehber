using MVC_Rehber.Entities;

namespace MVC_Rehber.Services.Repositories.Abstract
{
	public interface IRehberRepository
	{
		IQueryable<Rehber> Rehber { get; }
		Task<Rehber> GetByIdAsync(int id);
		Task<bool> RehberEkleAsync(Rehber rehber);
		Task<bool> RehberGuncelleAsync(Rehber rehber);
		Task<bool> RehberSilAsync(int id);
		Task<List<Rehber>> RehberUyeDahilEtAsync();
		Task<List<Uye>> UyeListesiAsync();
	}
}
