using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IProvaOpcaoRepository : IRepository<ProvaOpcao>
    {

        Task<IList<ProvaOpcao>> ObterPorQuestaoProvaAsync(int IdProvaQuestao);
        Task<ProvaOpcao> ObterComRelacionamentosAsync(int IdProvaQuestao);

    }
}
