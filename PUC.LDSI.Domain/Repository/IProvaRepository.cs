using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IProvaRepository : IRepository<Prova>
    {
        Task<Prova> ObterProvaDoAlunoAsync(int IdAvaliacao, int IdAluno);
        Task<IEnumerable<Prova>> ObterProvasComRelacoesAsync(int IdAluno);
    }
}
