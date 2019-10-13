using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoQuestaoService : IAvaliacaoQuestaoService
    {
        private readonly IAvaliacaoQuestaoRepository _avaliacaoQuestaoRepository;
        public AvaliacaoQuestaoService(IAvaliacaoQuestaoRepository avaliacaoQuestaoRepository)
        {
            _avaliacaoQuestaoRepository = avaliacaoQuestaoRepository;
        }
        
        public async Task<int> AdicionarAvaliacaoQuestaoAsync(string enunciado, int tipo, int id_avaliacao)
        {
            var avaliacaoQuestao = new AvaliacaoQuestao()
            {
                Enunciado = enunciado,
                Tipo = tipo,
                IdAvaliacao = id_avaliacao,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };
            _avaliacaoQuestaoRepository.Adicionar(avaliacaoQuestao);
            await _avaliacaoQuestaoRepository.SaveChangesAsync();
            return avaliacaoQuestao.Id;
        }

        public async Task<int> AlterarAvaliacaoQuestaoAsync(int id, string enunciado, int tipo, int id_avaliacao)
        {
            var avaliacaoQuestao = await _avaliacaoQuestaoRepository.ObterAsync(id);
            avaliacaoQuestao.Enunciado = enunciado;
            avaliacaoQuestao.Tipo = tipo;
            avaliacaoQuestao.IdAvaliacao = id_avaliacao;
            avaliacaoQuestao.AtualizadoEm = DateTime.Now;
            _avaliacaoQuestaoRepository.Modificar(avaliacaoQuestao);
            return await _avaliacaoQuestaoRepository.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            _avaliacaoQuestaoRepository.Remover(id);
            await _avaliacaoQuestaoRepository.SaveChangesAsync();
        }
    }
}
