using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.RegistrationService.Abstract;
using MVC_Rehber.Services.Repositories.Abstract;
using MVC_Rehber.ViewModels;

namespace MVC_Rehber.WebUI.Controllers
{
	public class MemberController : Controller
	{
		private readonly IUyeRepository _repository;
		private readonly IRehberRepository _rehberRepository;
		private readonly IRegistrationService _registrationService;
		private readonly IUpdateRegistrationService _updateRegistrationService;
		private readonly IEmailSender _emailSender;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public MemberController(IUyeRepository repository, IRehberRepository rehberRepository, IRegistrationService registrationService, IUpdateRegistrationService updateRegistrationService, IEmailSender emailSender, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_repository = repository;
			_rehberRepository = rehberRepository;
			_registrationService = registrationService;
			_updateRegistrationService = updateRegistrationService;
			_emailSender = emailSender;
			_signInManager = signInManager;
			_userManager = userManager;
		}
		Random rnd = new Random();

		[HttpGet]
		public IActionResult Add() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Add(Uye uye)
		{
			if(ModelState.IsValid)
			{
				RegisterViewModel model = new RegisterViewModel
				{
					Email = uye.MailAdresi,
					Password = rnd.Next(1,20).ToString() + Convert.ToChar(rnd.Next(65, 91)) + rnd.Next(10, 100).ToString() + Convert.ToChar(rnd.Next(97, 123))
				};
				model.ConfirmPassword = model.Password;

				IdentityResult result = await _registrationService.RegisterAsync(model);
				if (result.Succeeded)
				{
                    await _repository.UyeEkleAsync(uye);

                }
			}
                     return RedirectToAction("Index", "MemberManagement");
		}
	}
}
