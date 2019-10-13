using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace PUC.LDSI.Domain.Services
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        public DisciplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task<int> AdicionarDisciplinaAsync(string nome)
        {
            var disciplina = new Disciplina()
            {
                Nome = nome,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };
            _disciplinaRepository.Adicionar(disciplina);
            await _disciplinaRepository.SaveChangesAsync();
            return disciplina.Id;
        }
        
        public async Task<int> AlterarDisciplinaAsync(int id, string nome)
        {
            var disciplna = await _disciplinaRepository.ObterAsync(id);
            disciplna.Nome = nome;
            disciplna.AtualizadoEm = DateTime.Now;
            _disciplinaRepository.Modificar(disciplna);
            return await _disciplinaRepository.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var disciplina = await _disciplinaRepository.ObterComAvaliacaoAsync(id);
            if (disciplina.Avaliacoes?.Count > 0)
                throw new Exception("Não é possível excluir uma disciplina vinculada a uma avaliação!");
            _disciplinaRepository.Remover(id);
            await _disciplinaRepository.SaveChangesAsync();
        }
    }
}
