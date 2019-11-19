using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<Professor> ObterPorLogin(string login);
    }
}
