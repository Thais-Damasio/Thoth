using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IProvaQuestaoRepository : IRepository<ProvaQuestao>
    {
        Task<ProvaQuestao> ListarComRelacoesAsync(int idProvaQuestao);
    }
}
