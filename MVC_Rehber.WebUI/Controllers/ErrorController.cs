using Microsoft.AspNetCore.Mvc;

namespace MVC_Rehber.WebUI.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult ErrorPage()
		{
			return View();
		}
	}
}
