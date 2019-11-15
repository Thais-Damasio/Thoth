using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IAvaliacaoQuestaoRepository : IRepository<AvaliacaoQuestao>
    {
        Task<IList<AvaliacaoQuestao>> ObterComRelacoesAsync(int idAvalicao);
    }
}
