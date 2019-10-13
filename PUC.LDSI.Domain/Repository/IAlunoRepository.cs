using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Aluno ObterPorLogin(string login);
    }
}
