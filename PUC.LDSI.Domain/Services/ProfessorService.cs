using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }
        public async Task<int> IncluirNovoProfessorAsync(string email, string nome)
        {
            var professor = new Professor() { Nome = nome, Email = email };
            _professorRepository.Adicionar(professor);
            await _professorRepository.SaveChangesAsync();
            return professor.Id;
        }
    }
}
