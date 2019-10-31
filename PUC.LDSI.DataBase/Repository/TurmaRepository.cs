using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        private readonly AppDbContext _context;
        public TurmaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Turma> ObterComAlunosAsync(int id)
        {
            var turma = await _context.Turmas
           .Include(x => x.Alunos)
           .Where(x => x.Id == id).FirstOrDefaultAsync();
            return turma;
        }

        public async Task<IEnumerable<Turma>> ObterTurmasNaoPublicadas(int id_avaliacao)
        {
            var todasTurmas = await _context.Turmas.Include(p => p.Publicacoes).ToListAsync();

            var turmasFiltradas = from t in todasTurmas where !t.Publicacoes.Any(p => (p.IdAvaliacao == id_avaliacao)) select t;

            return turmasFiltradas;
        }
    }
}
