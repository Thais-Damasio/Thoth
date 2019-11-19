using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        private readonly AppDbContext _context;
        public ProfessorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Professor> ObterPorLogin(string login)
        {
            var retorno = await _context.Professores
           .Where(x => x.Email == login)
           .FirstOrDefaultAsync();
            return retorno;
        }
    }
}
