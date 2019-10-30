using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class DisciplinaController : BaseController
    {
        private readonly IDisciplinaService _disciplinaService;
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaController(IDisciplinaService disciplinaService, IDisciplinaRepository disciplinaRepository, UserManager<Usuario> _user) : base(_user)
        {
            _disciplinaService = disciplinaService;
            _disciplinaRepository = disciplinaRepository;
        }

        // GET: Disciplina
        public async Task<IActionResult> Index()
        {
            return View(await _disciplinaRepository.ListarTodosAsync());
        }

        // GET: Disciplina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _disciplinaRepository.ObterAsync(id.Value);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // GET: Disciplina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disciplina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                await _disciplinaService.AdicionarDisciplinaAsync(disciplina.Nome);
                return RedirectToAction(nameof(Index));
            }
            return View(disciplina);
        }

        // GET: Disciplina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var disciplina = await _disciplinaRepository.ObterAsync(id.Value);
            if (disciplina == null)
            {
                return NotFound();
            }
            return View(disciplina);
        }

        // POST: Disciplina/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _disciplinaService.AlterarDisciplinaAsync(disciplina.Id, disciplina.Nome);
                return RedirectToAction(nameof(Index));
            }
            return View(disciplina);
        }

        // GET: Disciplina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var turma = await _disciplinaRepository.ObterAsync(id.Value);
            if (turma == null)
            {
                return NotFound();
            }
            return View(turma);
        }

        // POST: Disciplina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _disciplinaService.ExcluirAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
