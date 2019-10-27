using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IProvaQuestaoRepository : IRepository<ProvaQuestao>
    {
        Task<int> AdicionarProvaQuestaoAsync(int IdAvaliacaoQuestao, int IdProva);
    }
}
