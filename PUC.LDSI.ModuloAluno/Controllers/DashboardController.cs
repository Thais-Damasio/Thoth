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

namespace PUC.LDSI.ModuloAluno.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        public DashboardController(
            IPublicacaoRepository avaliacaoRepository,
            UserManager<Usuario> _user) : base(_user)
        {
            _publicacaoRepository = avaliacaoRepository;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            return View(await _publicacaoRepository.ListarComRelacoesAsync());
        }
    }
}