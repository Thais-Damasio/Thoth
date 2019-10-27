using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Aluno ObterPorLogin(int login);
        Task<List<Publicacao>> ObterPulicacoesPorAlunoAsync(int id);

    }
}
