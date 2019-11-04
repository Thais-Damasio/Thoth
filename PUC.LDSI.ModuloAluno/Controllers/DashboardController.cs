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
        private readonly IAlunoRepository _alunoRepository;

        public DashboardController(
            IPublicacaoRepository avaliacaoRepository,
            IAlunoRepository alunoRepository,
            UserManager<Usuario> _user) : base(_user)
        {
            _publicacaoRepository = avaliacaoRepository;
            _alunoRepository = alunoRepository;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            Aluno aluno = await _alunoRepository.ObterPorLogin(LoginUsuario.Email);
            return View(await _publicacaoRepository.ListarComRelacoesAsync(aluno.IdTurma));
        }
    }
}