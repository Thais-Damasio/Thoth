using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<Aluno> ObterPorLogin(string login);
    }
}
