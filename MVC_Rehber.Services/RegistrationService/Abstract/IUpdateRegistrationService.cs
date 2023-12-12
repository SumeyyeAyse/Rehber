using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Rehber.Services.RegistrationService.Abstract
{
	public interface IUpdateRegistrationService
	{
		Task UpdatePasswordAsync(string id, string currentPassword,string newPassword);
		Task UpdateAsync(string id);
		Task UpdateAsync(string id, string mail);
		Task RemoveAsync(string id);
	}
}
