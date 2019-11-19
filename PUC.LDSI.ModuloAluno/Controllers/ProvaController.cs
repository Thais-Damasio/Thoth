using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.QueryResult;
using PUC.LDSI.Domain.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.ModuloAluno.Controllers
{
    public class ProvaController : BaseController
    {
        private readonly IProvaService _ProvaService;
        private readonly IAlunoService _AlunoService;
        private readonly IAvaliacaoService _AvalicaoService;

        public ProvaController(
            AppDbContext context,
            IProvaService provaService,
            IAlunoService alunoService,
            IAvaliacaoService avaliacaoService,
            UserManager<Usuario> _user) : base(_user)
        {
            _ProvaService = provaService;
            _AlunoService = alunoService;
            _AvalicaoService = avaliacaoService;
        }

        // GET: Prova
        public async Task<IActionResult> Index()
        {
            //var aluno = await _provaService.ObterPorLogin(LoginUsuario.Email);
            return View(await _ProvaService.ObterProvasComRelacoes(LoginUsuario.Email));
        }

        // GET: Prova/FazerProva
        public async Task<IActionResult> FazerProva(int id)
        {
            var prova = await _ProvaService.ObterProvaAsync(id, LoginUsuario.Email);

            return View("Prova", prova);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarProva([Bind("AvaliacaoId,PublicacaoId,Questoes")] ProvaQueryResult prova)
        {
            if (ModelState.IsValid)
            {
                await _ProvaService.SalvarProvaAsync(prova, LoginUsuario.Email);
                return RedirectToAction(nameof(Index));
            }
            ProvaQueryResult provaBd = await _ProvaService.ObterProvaAsync(prova.PublicacaoId, LoginUsuario.Email);
            for (int i = 0; i < prova.Questoes.Count; i++)
            {
                provaBd.Questoes[i].Completa = prova.Questoes[i].Completa;
                for (int j = 0; j < prova.Questoes[i].Opcoes.Count; j++)
                {
                    provaBd.Questoes[i].Opcoes[j].Resposta = prova.Questoes[i].Opcoes[j].Resposta;
                }
            }
            return View("Prova", provaBd);
        }
    }
}
