using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;

namespace PUC.LDSI.ModuloProfessor.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public DashboardController(
            IAvaliacaoService avaliacaoService,
            IAvaliacaoRepository avaliacaoRepository,
            UserManager<Usuario> _user) : base(_user)
        {
            _avaliacaoService = avaliacaoService;
            _avaliacaoRepository = avaliacaoRepository;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            return View(await _avaliacaoRepository.ListarComRelacoesAsync());
        }
    }
}