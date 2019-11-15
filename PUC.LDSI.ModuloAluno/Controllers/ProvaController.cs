using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
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
        private AppDbContext _context;

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

        // GET: Avaliacao
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Avaliacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avaliacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProva([Bind("IdAluno,IdAvaliacao")] Prova prova)
        {
            if (ModelState.IsValid)
            {
                var aluno = _AlunoService.BuscarPorEmail(LoginUsuario.Email);
                prova.IdAluno = aluno.Id;
                
                int id_Prova = await _ProvaService.IncluirNovaProvaAsync(prova.IdAvaliacao, prova.IdAvaliacao, prova.IdAluno);

                return RedirectToAction(nameof(EditQuestaoProva), new { id = id_Prova });
            }
            return View(prova);
        }


        // POST: Avaliacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestao([Bind("IdProva,IdAvaliacaoQuestao")] ProvaQuestao questao)
        {
            if (ModelState.IsValid)
            {
                var aluno = _AlunoService.BuscarPorEmail(LoginUsuario.Email);
                questao.IdProva = aluno.Id;

                int id_ProvaQuestao = await _ProvaService.IncluirNovaProvaQuestaoAsync(questao.IdAvaliacaoQuestao, questao.IdProva);

                return RedirectToAction(nameof(EditQuestaoProva), new { id = id_ProvaQuestao });
            }
            return View(questao);
        }


        // POST: Avaliacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestaoProva(int id, [Bind("IdProva,IdAvaliacaoQuestao")] ProvaQuestao questao)
        {
            if (id != questao.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _ProvaService.AtualizarNotaProvaQuestaoAsync(questao.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(questao);
        }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProvaOpcao([Bind("IdQuestaoProva,IdAvaliacaoOpcao,Resposta")] ProvaOpcao opcao)
        {
            if (ModelState.IsValid)
            {
               
                int id_ProvaOpacao = await _ProvaService.IncluirNovaProvaOpcaoAsync(opcao.IdAvaliacaoOpcao, opcao.IdQuestaoProva, opcao.Resposta);

                return RedirectToAction(nameof(EditProvaOpcao), new { id = id_ProvaOpacao });
            }
            return View(opcao);
        }

        // POST: Avaliacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProvaOpcao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provaOpcao = await _ProvaService.ObterProvaOpcaoAsync(id.Value);
            if (provaOpcao == null)
            {
                return NotFound();
            }
            return View(provaOpcao);
        }

        // GET: Avaliacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAvaliacaoQuestoes(int? idAvaliacao)
        {
            if (idAvaliacao == null)
            {
                return NotFound();
            }

            var questoes = await _AvalicaoService.ObterAvaliacaoQuestaoAsync(idAvaliacao.Value);
            if (questoes == null)
            {
                return NotFound();
            }
            return View(questoes);
        }

    }
}
