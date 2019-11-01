using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Services.Interfaces;
using System.Threading.Tasks;

namespace PUC.LDSI.ModuloAluno.Controllers
{
    public class AlunosController : BaseController
    {
        private readonly IAlunoService _alunoService;


        public AlunosController(IAlunoService alunoService,
                                UserManager<Usuario> user):base(user)
        {
            _alunoService = alunoService;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            return View(_alunoService.ObterAlunosAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunoService.ObterAlunoDetailsAsync(id.Value);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Matricula,Senha,IdTurma,Id,CriadoEm,AtualizadoEm")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                await _alunoService.IncluirNovoAlunoAsync(aluno);
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunoService.ObterAlunoAsync(id.Value);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Matricula,Senha,IdTurma,Id,CriadoEm,AtualizadoEm")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _alunoService.AlterarAlunoAsync(aluno);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
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
            return View(aluno);
        }

       
        // GET: Avaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunoService.ObterAlunoAsync(id.Value);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }


        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alunoService.ExcluirAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _alunoService.ObterAlunoAsync(id) != null;
        }
    }
}
