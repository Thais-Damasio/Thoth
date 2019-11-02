using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;

namespace PUC.LDSI.ModuloAluno.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IAlunoService _AlunoService;

        public DashboardController(IAlunoService alunoService,
                                   UserManager<Usuario> user) : base(user)
        {
            _AlunoService = alunoService;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            return View(await _AlunoService.ObterPulicacoesPorAlunoAsync(this.LoginUsuario.Email));
        }

    }
}