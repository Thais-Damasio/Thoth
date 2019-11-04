using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class PublicacaoController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IPublicacaoService _publicacaoService;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public PublicacaoController(
            AppDbContext context,
            IPublicacaoService publicacaoService,
            IAvaliacaoRepository avaliacaoRepository,
            IPublicacaoRepository publicacaoRepository,
            ITurmaRepository turmaRepository,
            UserManager<Usuario> _user) : base(_user)
        {
            _context = context;
            _publicacaoService = publicacaoService;
            _avaliacaoRepository = avaliacaoRepository;
            _publicacaoRepository = publicacaoRepository;
            _turmaRepository = turmaRepository;
        }

        // GET: Publicacao/
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

        // GET: Publicacao/Create/3
        public async Task<IActionResult> Create(int? id)
        {
            //id davaliação de destino
            if (id == null)
            {
                return NotFound();
            }

            Avaliacao av = await _avaliacaoRepository.ObterComPublicacaoAsync(id.Value);
            if(av == null)
            {
                return NotFound();
            }

            ViewData["IdAvaliacao"] = av.Id;
            ViewData["IdTurma"] = new MultiSelectList(await _turmaRepository.ObterTurmasNaoPublicadas(av.Id), "Id", "Nome");
            return View();
        }

        // POST: Publicacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataInicio,DataFim,Valor,IdAvaliacao,Turmas")] PublicacaoViewModel publicacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _publicacaoService.IncluirNovaPublicacaoAsync(
                        publicacao.DataInicio.Value, publicacao.DataFim.Value, publicacao.Valor.Value, publicacao.IdAvaliacao, publicacao.Turmas);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdAvaliacao"] = publicacao.IdAvaliacao;
                ViewData["IdTurma"] = new MultiSelectList(await _turmaRepository.ObterTurmasNaoPublicadas(publicacao.IdAvaliacao), "Id", "Nome");
                return View(publicacao);
            }
            catch(Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                ViewData["IdAvaliacao"] = publicacao.IdAvaliacao;
                ViewData["IdTurma"] = new MultiSelectList(await _turmaRepository.ObterTurmasNaoPublicadas(publicacao.IdAvaliacao), "Id", "Nome");
                return View(publicacao);

            }
        }

        // GET: Publicacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _publicacaoRepository.ObterComRelacoesAsync(id.Value);
            if (publicacao == null)
            {
                return NotFound();
            }
            return View(publicacao);
        }

        // POST: Publicacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataInicio,DataFim,Valor,Id")] Publicacao publicacao)
        {
            if (id != publicacao.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _publicacaoService.AlterarPublicacaoAsync(
                    publicacao.Id, publicacao.DataInicio.Value, publicacao.DataFim.Value, publicacao.Valor.Value);
                return RedirectToAction(nameof(Index));
            }
            var publicacaoCompleta = await _publicacaoRepository.ObterComRelacoesAsync(id);
            return View(publicacaoCompleta);
        }

        // GET: Publicacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var publicacao = await _publicacaoRepository.ObterComRelacoesAsync(id.Value);
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
            await _publicacaoService.ExcluirAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
    public class PublicacaoViewModel
    {
        //Atributos
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int? Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Avaliação")]
        public int IdAvaliacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Turma")]
        public IEnumerable<int> Turmas { get; set; }
    }
}
