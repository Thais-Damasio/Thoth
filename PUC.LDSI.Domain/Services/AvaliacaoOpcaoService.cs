using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoOpcaoService : IAvaliacaoOpcaoService
    {
        private readonly IAvaliacaoOpcaoRepository _avaliacaoOpcaoRepository;
        public AvaliacaoOpcaoService(IAvaliacaoOpcaoRepository avaliacaoOpcaoRepository)
        {
            _avaliacaoOpcaoRepository = avaliacaoOpcaoRepository;
        }

        public async Task<int> AdicionarAvaliacaoOpcaoAsync(string descricao, bool resposta, int id_avaliacao_opcao)
        {
            var avaliacaoOpcao = new AvaliacaoOpcao()
            {
                IdAvaliacaoQuestao = id_avaliacao_opcao,
                Descricao = descricao,
                Resposta = resposta,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };
            _avaliacaoOpcaoRepository.Adicionar(avaliacaoOpcao);
            await _avaliacaoOpcaoRepository.SaveChangesAsync();
            return avaliacaoOpcao.Id;
        }
        
        public async Task<int> AlterarAvaliacaoOpcaoAsync(int id, string descricao, bool resposta, int id_avaliacao_opcao)
        {
            var avaliacao = await _avaliacaoOpcaoRepository.ObterAsync(id);
            avaliacao.Descricao = descricao;
            avaliacao.Resposta = resposta;
            avaliacao.IdAvaliacaoQuestao = id_avaliacao_opcao;
            avaliacao.AtualizadoEm = DateTime.Now;
            _avaliacaoOpcaoRepository.Modificar(avaliacao);
            return await _avaliacaoOpcaoRepository.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            _avaliacaoOpcaoRepository.Remover(id);
            await _avaliacaoOpcaoRepository.SaveChangesAsync();
        }
    }
}
