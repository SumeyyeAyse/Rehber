﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVC_Rehber.Entities;
using MVC_Rehber.Services.Repositories.Abstract;
using MVC_Rehber.ViewModels;

namespace MVC_Rehber.WebUI.Controllers
{
	public class AccountController :  Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IUyeRepository _uyeRepository;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, IUyeRepository uyeRepository)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_uyeRepository = uyeRepository;
		}

		[HttpGet]
		public IActionResult Register() => View();
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = new IdentityUser
				{
					UserName = model.Email,
					Email = model.Email
				};
				IdentityResult result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					Uye uye = new Uye
					{
						MailAdresi = model.Email,
						Ad = "Adınız",
						Soyad = "Soyadınız",
						KullaniciAdi = "Kullanıcı adınız"
					};
					if(!_uyeRepository.Roles.Any(x => x.Name == "kullanici"))
					{
						var role = new IdentityRole
						{
							Name = "kullanici",
							NormalizedName = "KULLANICI",
							ConcurrencyStamp = Guid.NewGuid().ToString()
						};
						await _uyeRepository.AddRole(role);
					}

					await _uyeRepository.UyeEkleAsync(uye);
					await _userManager.AddToRoleAsync(user, "kullanici");
					//Guid activationCode = Guid.NewGuid();
					//await _emailSender.SendEmailAsync(user.Email, "Aktivasyon Maili", "<br /><a href = '" + string.Format("https://localhost:44318/Activation/Activation/{0}", activationCode) + "'>Üye olmak için tıklayınız</a>");

					await _emailSender.SendEmailAsync(user.Email, "Üyelik İşlemleri", "<br />Üyelik işleminiz başarıyla gerçekleşmiştir.");
					return RedirectToAction("Index", "Home");
				}
				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				ModelState.AddModelError(string.Empty, "Geçersiz Giriş Denemesi");
			}
			return View(model);
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login() => View();

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel user)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
				if (result.Succeeded)
				{
					Uye uye = _uyeRepository.Uye.FirstOrDefault(x => x.MailAdresi == user.Email);
					HttpContext.Session.SetInt32("id", uye.UyeID);
					IdentityUser userid = _userManager.Users.FirstOrDefault(x => x.Email == user.Email);
					HttpContext.Session.SetString("accountId", await _userManager.GetUserIdAsync(userid));	
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError(string.Empty, "Geçersiz Giriş Denemesi");
			}
			return View(user);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		public IActionResult AccessDenied() => View();
	}
}
