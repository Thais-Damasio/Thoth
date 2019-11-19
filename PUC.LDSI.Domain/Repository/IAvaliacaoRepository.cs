using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IAvaliacaoRepository : IRepository<Avaliacao>
    {
        Task<Avaliacao> ObterComPublicacaoAsync(int id);
        Task<Avaliacao> ObterComRelacoesAsync(int id);
        Task<IEnumerable<Avaliacao>> ListarComRelacoesAsync(int id_professor);
    }
}
