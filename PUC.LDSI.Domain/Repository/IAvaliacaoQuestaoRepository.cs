using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IAvaliacaoQuestaoRepository
    {
        Task<int> IncluirNovoProfessorAsync(Professor professor);
        Professor ObterPorLogin(string login);
    }
}
