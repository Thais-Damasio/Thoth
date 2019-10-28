using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;

namespace PUC.LDSI.ModuloProfessor.Controllers
{
    public class AvaliacaoController : BaseController
    {
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly IProfessorService _professorService;
        private readonly IAvaliacaoQuestaoService _avaliacaoQuestaoService;
        private readonly IAvaliacaoOpcaoService _avaliacaoOpcaoService;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private AppDbContext _context;

        public AvaliacaoController(
            AppDbContext context,
            IAvaliacaoService avaliacaoService,
            IProfessorService professorService,
            IAvaliacaoQuestaoService avaliacaoQuestaoService,
            IAvaliacaoOpcaoService avaliacaoOpcaoService,
            IAvaliacaoRepository avaliacaoRepository,
            UserManager<Usuario> _user) : base(_user)
        {
            _context = context;
            _avaliacaoService = avaliacaoService;
            _professorService = professorService;
            _avaliacaoOpcaoService = avaliacaoOpcaoService;
            _avaliacaoQuestaoService = avaliacaoQuestaoService;
            _avaliacaoRepository = avaliacaoRepository;
        }

        // GET: Avaliacao
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Avaliacao/Create
        public IActionResult Create()
        {
            ViewData["IdDisciplina"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            return View();
        }

        // GET: Avaliacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _avaliacaoRepository.ObterComRelacoesAsync(id.Value);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }


        //[HttpPost]
        //public ActionResult Create([Bind("Materia,Descricao,IdDisciplina,Id,Questoes")] Avaliacao avaliacao)
        //{

        //    return RedirectToAction(nameof(View));
        //}

        // POST: Avaliacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Materia,Descricao,IdDisciplina,Questoes")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                string email = _userManager.GetUserName(User);
                var professor = _professorService.BuscarPorEmail(email);
                avaliacao.IdProfessor = professor.Id;


                int id_avaliacao = await _avaliacaoService.AdicionarAvaliacaoAsync(avaliacao.Materia, avaliacao.Descricao, avaliacao.IdProfessor, avaliacao.IdDisciplina);
                if (avaliacao.Questoes != null && avaliacao.Questoes.Count > 0)
                    foreach (var questao in avaliacao.Questoes)
                    {
                        int id_opcao = await _avaliacaoQuestaoService.AdicionarAvaliacaoQuestaoAsync(questao.Enunciado, questao.Tipo, id_avaliacao);
                        if (questao.Opcoes != null && questao.Opcoes.Count > 0)
                            foreach (var opcao in questao.Opcoes)
                            {
                                await _avaliacaoOpcaoService.AdicionarAvaliacaoOpcaoAsync(opcao.Descricao, opcao.Resposta, id_opcao);
                            }
                    }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDisciplina"] = new SelectList(_context.Disciplinas, "Id", "Nome", avaliacao.IdDisciplina);
            ViewData["IdProfessor"] = new SelectList(_context.Professores, "Id", "Email", avaliacao.IdProfessor);
            return View(avaliacao);
        }

        // GET: Avaliacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _avaliacaoRepository.ObterComRelacoesAsync(id.Value);
            if (avaliacao == null)
            {
                return NotFound();
            }
            ViewData["IdDisciplina"] = new SelectList(_context.Disciplinas, "Id", "Nome", avaliacao.IdDisciplina);
            ViewData["IdProfessor"] = new SelectList(_context.Professores, "Id", "Email", avaliacao.IdProfessor);
            return View(avaliacao);
        }

        // POST: Avaliacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Materia,Descricao,IdProfessor,IdDisciplina,Id,CriadoEm,AtualizadoEm")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDisciplina"] = new SelectList(_context.Disciplinas, "Id", "Id", avaliacao.IdDisciplina);
            ViewData["IdProfessor"] = new SelectList(_context.Professores, "Id", "Id", avaliacao.IdProfessor);
            return View(avaliacao);
        }

        // GET: Avaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Disciplina)
                .Include(a => a.Professor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
