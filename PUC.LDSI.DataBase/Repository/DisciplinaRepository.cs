using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;
using PUC.LDSI.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PUC.LDSI.DataBase.Repository
{
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        private readonly AppDbContext _context;
        public DisciplinaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Disciplina> ObterComAvaliacaoAsync(int id)
        {
            var disciplinas = await _context.Disciplinas
           .Include(x => x.Avaliacoes)
           .Where(x => x.Id == id).FirstOrDefaultAsync();
            return disciplinas;
        }
    }
}

