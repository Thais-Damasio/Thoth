using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.ModuloProfessor.Controllers
{
    public class PublicacaoController : Controller
    {
        private readonly AppDbContext _context;

        public PublicacaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Publicacao
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Publicacoes.Include(p => p.Avaliacao).Include(p => p.Turma);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Publicacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacoes
                .Include(p => p.Avaliacao)
                .Include(p => p.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacao == null)
            {
                return NotFound();
            }

            return View(publicacao);
        }

        // GET: Publicacao/Create
        public IActionResult Create()
        {
            ViewData["IdAvaliacao"] = new SelectList(_context.Avaliacoes, "Id", "Descricao");
            ViewData["IdTurma"] = new SelectList(_context.Turmas, "Id", "Nome");
            return View();
        }

        // POST: Publicacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataInicio,DataFim,Valor,IdAvaliacao,IdTurma,Id,CriadoEm,AtualizadoEm")] Publicacao publicacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAvaliacao"] = new SelectList(_context.Avaliacoes, "Id", "Descricao", publicacao.IdAvaliacao);
            ViewData["IdTurma"] = new SelectList(_context.Turmas, "Id", "Nome", publicacao.IdTurma);
            return View(publicacao);
        }

        // GET: Publicacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacoes.FindAsync(id);
            if (publicacao == null)
            {
                return NotFound();
            }
            ViewData["IdAvaliacao"] = new SelectList(_context.Avaliacoes, "Id", "Descricao", publicacao.IdAvaliacao);
            ViewData["IdTurma"] = new SelectList(_context.Turmas, "Id", "Nome", publicacao.IdTurma);
            return View(publicacao);
        }

        // POST: Publicacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataInicio,DataFim,Valor,IdAvaliacao,IdTurma,Id,CriadoEm,AtualizadoEm")] Publicacao publicacao)
        {
            if (id != publicacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacaoExists(publicacao.Id))
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
            ViewData["IdAvaliacao"] = new SelectList(_context.Avaliacoes, "Id", "Descricao", publicacao.IdAvaliacao);
            ViewData["IdTurma"] = new SelectList(_context.Turmas, "Id", "Nome", publicacao.IdTurma);
            return View(publicacao);
        }

        // GET: Publicacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacoes
                .Include(p => p.Avaliacao)
                .Include(p => p.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacao == null)
            {
                return NotFound();
            }

            return View(publicacao);
        }

        // POST: Publicacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicacao = await _context.Publicacoes.FindAsync(id);
            _context.Publicacoes.Remove(publicacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacaoExists(int id)
        {
            return _context.Publicacoes.Any(e => e.Id == id);
        }
    }
}
