using Compras.Datos.Entities;
using Compras.Helpers;
using Compras.Models;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Controllers
{
   
    public class PruebasController :Controller  
    {
        private readonly IUserHelper _userHelper;
        public PruebasController(IUserHelper userHelper)

        {
            _userHelper = userHelper;
        }
        public async Task<IActionResult> LoginPrueba()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userHelper.LogoutAsync();
                return RedirectToAction("LoginPrueba", "Pruebas");
            }

            return View(new LoginViewModel());

        }
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
    }

 
}
