using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace PUC.LDSI.DataBase.Repository
{
    public class PublicacaoRepository : Repository<Publicacao>, IPublicacaoRepository
    {
        private readonly AppDbContext _context;
        public PublicacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Publicacao> ObterComRelacoesAsync(int id)
        {
            var publicacao = await _context.Publicacoes
               .Include(a => a.Avaliacao).ThenInclude(a => a.Questoes).ThenInclude(q => q.Opcoes)
               .Include(t => t.Turma)
               .ThenInclude(t => t.Alunos)
               .Where(x => x.Id == id).FirstOrDefaultAsync();
            return publicacao;
        }

        public async Task<IEnumerable<Publicacao>> ListarComRelacoesAsync()
        {
            var publicacoes = await _context.Publicacoes
           .Include(p => p.Avaliacao).ThenInclude(a => a.Disciplina)
           .Include(p => p.Turma)
           .ToListAsync();
            return publicacoes;
        }

        public async Task<IEnumerable<Publicacao>> ListarComRelacoesAsync(int id_turma, int id_aluno)
        {
            var publicacoes = await _context.Publicacoes
           .Include(p => p.Avaliacao).ThenInclude(a => a.Disciplina)
           .Include(p => p.Provas)
           .Where(p => p.IdTurma == id_turma)
           .Where(p => p.Provas.Any(i => i.IdAluno == id_aluno) || p.Provas.Count == 0)
           .ToListAsync();
            return publicacoes;
        }

        public async Task<IEnumerable<Publicacao>> ObterProvasComRelacoesAsync(int IdAluno, int id_turma)
        {
            var provas = await _context.Publicacoes
           .Include(p => p.Avaliacao)
           .Include(p => p.Provas).ThenInclude(x => x.Questoes)
           .Where(p => p.Provas.Any(i => i.IdAluno == IdAluno) || p.DataFim <= DateTime.Now)
           .Where(p => p.IdTurma == id_turma)
           .ToListAsync();
            return provas;
        }
    }
}
