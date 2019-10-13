using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<Disciplina> ObterComAvaliacaoAsync(int id);
    }
}
