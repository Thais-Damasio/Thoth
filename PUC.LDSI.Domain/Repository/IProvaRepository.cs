using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IProvaRepository : IRepository<Prova>
    {
        Task<int> AdicionarProvaAsync(int idAluno, int idPublicacao);
        Task<Prova> ObterProvaComAlunoTurmaAsync(int IdProva);
    }
}
