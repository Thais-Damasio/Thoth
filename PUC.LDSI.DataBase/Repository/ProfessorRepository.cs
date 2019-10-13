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
        public Professor ObterPorLogin(string login)
        {
            var retorno = _context.Professores
           .Where(x => x.Email == login).FirstOrDefault();
            return retorno;
        }
    }
}
