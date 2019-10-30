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
    public class AvaliacaoQuestaoController : BaseController
    {
        private readonly IAvaliacaoQuestaoService _avaliacaoQuestaoService;
        private readonly IAvaliacaoOpcaoService _avaliacaoOpcaoService;
        private readonly IAvaliacaoRepository _avaliacaoRepo;
        private AppDbContext _context;

        public AvaliacaoQuestaoController(
            IAvaliacaoQuestaoService avaliacaoQuestaoService,
            IAvaliacaoOpcaoService avaliacaoOpcaoService,
            IAvaliacaoRepository avaliacaoRepo,
            UserManager<Usuario> _user,
            AppDbContext context) : base(_user)
        {
            _avaliacaoQuestaoService = avaliacaoQuestaoService;
            _avaliacaoOpcaoService = avaliacaoOpcaoService;
            _avaliacaoRepo = avaliacaoRepo;
            _context = context;
        }

        // POST: AvaliacaoQuestao/CreateByAvaliacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByAvaliacao([Bind("id_avaliacao")] int id_avaliacao)
        {
            Avaliacao avaliacao = await _avaliacaoRepo.ObterAsync(id_avaliacao);
            ViewData["Avaliacao"] = avaliacao;

            return View("Create");
        }

        // POST: AvaliacaoQuestao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Enunciado,IdAvaliacao,Opcoes")] AvaliacaoQuestao questao)
        {
            Avaliacao avaliacao = await _avaliacaoRepo.ObterAsync(questao.IdAvaliacao);
            ViewData["Avaliacao"] = avaliacao;
            try
            {
                if (ModelState.IsValid)
                {
                    int id_opcao = await _avaliacaoQuestaoService.AdicionarAvaliacaoQuestaoAsync(questao.Enunciado, questao.Tipo, questao.IdAvaliacao, questao.Opcoes);
                    return RedirectToAction("Edit", "Avaliacao", new { id = questao.IdAvaliacao });
                }
                return View(questao);
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return View(questao);
            }
        }

        // GET: AvaliacaoQuestao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoQuestao = await _context.AvaliacaoQuestoes
                .Include(a => a.Avaliacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacaoQuestao == null)
            {
                return NotFound();
            }

            return View(avaliacaoQuestao);
        }

        // POST: AvaliacaoQuestao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacaoQuestao = await _context.AvaliacaoQuestoes.FindAsync(id);
            _context.AvaliacaoQuestoes.Remove(avaliacaoQuestao);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Avaliacao", new { id = avaliacaoQuestao.IdAvaliacao });
        }

        private bool AvaliacaoQuestaoExists(int id)
        {
            return _context.AvaliacaoQuestoes.Any(e => e.Id == id);
        }
    }
}
