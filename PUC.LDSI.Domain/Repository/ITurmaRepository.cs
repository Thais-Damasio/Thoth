using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        Task<Turma> ObterComAlunosAsync(int id);
        Task<IEnumerable<Turma>> ObterTurmasNaoPublicadas(int id_avaliacao);
    }
}
