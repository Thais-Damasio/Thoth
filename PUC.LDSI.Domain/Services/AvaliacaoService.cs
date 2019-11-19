using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using System.Threading.Tasks;
using System;
using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IAvaliacaoQuestaoRepository _avaliacaoQuestaoRepository;
        public AvaliacaoService(
            IAvaliacaoRepository avaliacaoRepository,
            IAvaliacaoQuestaoRepository avaliacaoQuestaoRepository,
            IProfessorRepository professorRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _avaliacaoQuestaoRepository = avaliacaoQuestaoRepository;
            _professorRepository = professorRepository;
        }
        public async Task<IEnumerable<Avaliacao>> ListarComRelacoesAsync(string email)
        {
            Professor professor = await _professorRepository.ObterPorLogin(email);

            if (professor == null)
                throw new Exception("Professor não encontrado");

            return await _avaliacaoRepository.ListarComRelacoesAsync(professor.Id);
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

        public async Task<IList<AvaliacaoQuestao>> ObterAvaliacaoQuestaoAsync(int idAvaliacao)
        {
            var avaliacao = await _avaliacaoQuestaoRepository.ObterComRelacoesAsync(idAvaliacao);
            return avaliacao;
        }

        public async Task<Avaliacao> ObterComRelacoesAsync(int id)
        {
            var avaliacao = await _avaliacaoRepository.ObterComRelacoesAsync(id);
            return avaliacao;
        }
    }
}
