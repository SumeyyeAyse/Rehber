using Microsoft.AspNetCore.Mvc;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.Repositories.Abstract;

namespace MVC_Rehber.WebUI.Controllers
{
	public class ActivationController : Controller
	{
		private readonly IUyeRepository _uyeRepository;
		public ActivationController(IUyeRepository uyeRepository)
		{
			_uyeRepository = uyeRepository;
		}

		[HttpGet]
		public ActionResult Activation()
		{
			Uye uye = _uyeRepository.Uye.FirstOrDefault(x => x.MailAdresi == (string)TempData["uyeMail"]);
			return View();
		}
	}
}
