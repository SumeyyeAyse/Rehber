using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.Repositories.Abstract;
using MVC_Rehber.WebUI.Models;
using System.Diagnostics;

namespace MVC_Rehber.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUyeRepository _uyeRepository;
		private readonly IRehberRepository _rehberRepository;

		public HomeController(ILogger<HomeController> logger, IUyeRepository uyeRepository, IRehberRepository rehberRepository)
		{
			_logger = logger;
			_uyeRepository = uyeRepository;
			_rehberRepository = rehberRepository;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View(Tuple.Create<IEnumerable<Rehber>, IEnumerable<Uye>>(_rehberRepository.Rehber.ToList(), _uyeRepository.Uye.ToList()));
		}

		[HttpPost]
		public IActionResult Index([Bind(Prefix = "item1")] Rehber item1, [Bind(Prefix = "item2")] Uye item2) => View();
		public IActionResult Uye() => View();
		public IActionResult RehberListesi(int id)
		{
			IEnumerable<Rehber> rehberListesi = _rehberRepository.Rehber.Where(x => x.UyeID == id);
			return View(rehberListesi);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}