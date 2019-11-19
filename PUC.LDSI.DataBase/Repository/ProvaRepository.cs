using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.DataBase.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaRepository : Repository<Prova>, IProvaRepository
    {
        private readonly AppDbContext _context;
        public ProvaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Prova> ObterProvaDoAlunoAsync(int IdAvaliacao, int IdAluno)
        {
            var prova = await _context.Provas
           .Include(x => x.Questoes).ThenInclude(x => x.Opcoes)
           .Where(x => x.IdAluno == IdAluno)
           .Where(x => x.IdAvaliacao == IdAvaliacao)
           .FirstOrDefaultAsync();

            return prova;
        }

        public async Task<IEnumerable<Prova>> ObterProvasComRelacoesAsync(int IdAluno)
        {
            var provas = await _context.Provas
           .Include(x => x.Publicacao)
           .Include(p => p.Avaliacao)
           .Include(x => x.Questoes)
           .Where(x => x.IdAluno == IdAluno)
           .ToListAsync();
            return provas;
        }
    }
}
