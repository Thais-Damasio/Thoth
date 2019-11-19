using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IProfessorService
    {
        Task<int> IncluirNovoProfessorAsync(string email, string nome);
        Task<Professor> BuscarPorEmail(string email);
    }
}
