using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using System.Threading.Tasks;
using System;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<int> AdicionarAvaliacaoAsync(string materia, string descricao, int id_professor, int id_disciplina)
        {
            var avaliacao = new Avaliacao() {
                Materia = materia,
                Descricao = descricao,
                IdDisciplina = id_disciplina,
                IdProfessor = id_professor,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };
            _avaliacaoRepository.Adicionar(avaliacao);
            await _avaliacaoRepository.SaveChangesAsync();
            return avaliacao.Id;
        }

        public async Task<int> AlterarAvaliacaoAsync(int id, string materia, string descricao, int id_disciplina)
        {
            var avaliacao = await _avaliacaoRepository.ObterAsync(id);
            avaliacao.Materia = materia;
            avaliacao.Descricao = descricao;
            avaliacao.IdDisciplina = id_disciplina;
            avaliacao.AtualizadoEm = DateTime.Now;
            _avaliacaoRepository.Modificar(avaliacao);
            return await _avaliacaoRepository.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var avaliacao = await _avaliacaoRepository.ObterComPublicacaoAsync(id);
            if (avaliacao.Publicacoes?.Count > 0)
                throw new Exception("Não é possível excluir uma avaliação que já foi publicada!");
            _avaliacaoRepository.Remover(id);
            await _avaliacaoRepository.SaveChangesAsync();
        }
    }
}
