using Microsoft.AspNetCore.Identity;
using MVC_Rehber.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Rehber.Services.RegistrationService.Abstract
{
	public interface IRegistrationService
	{
		Task<IdentityResult> RegisterAsync(RegisterViewModel model);
	}
}
