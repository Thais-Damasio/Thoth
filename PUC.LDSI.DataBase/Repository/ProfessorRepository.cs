using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;
        public ProfessorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> IncluirNovoProfessorAsync(Professor professor)
        {
            _context.Add(professor);
            var retorno = await _context.SaveChangesAsync();
            return retorno;
        }
        public Professor ObterPorLogin(string login)
        {
            var retorno = _context.Professores
           .Where(x => x.Email == login).FirstOrDefault();
            return retorno;
        }
    }
}
