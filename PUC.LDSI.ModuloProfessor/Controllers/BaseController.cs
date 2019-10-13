using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.ModuloProfessor.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<Usuario> _userManager;
        protected Usuario LoginUsuario => _userManager.GetUserAsync(User).Result;
        public BaseController(UserManager<Usuario> user)
        {
            _userManager = user;
        }
    }
}
