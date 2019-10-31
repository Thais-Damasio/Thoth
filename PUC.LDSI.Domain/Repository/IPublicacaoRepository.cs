using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IPublicacaoRepository : IRepository<Publicacao>
    {
        Task<Publicacao> ObterComRelacoesAsync(int id);
    }
}
