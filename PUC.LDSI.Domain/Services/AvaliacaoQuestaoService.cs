using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoQuestaoService : IAvaliacaoQuestaoService
    {
        private readonly IAvaliacaoQuestaoRepository _avaliacaoQuestaoRepository;
        private readonly IAvaliacaoOpcaoService _avaliacaoOpcaoService;
        public AvaliacaoQuestaoService(
            IAvaliacaoQuestaoRepository avaliacaoQuestaoRepository,
            IAvaliacaoOpcaoService avaliacaoOpcaoService)
        {
            _avaliacaoQuestaoRepository = avaliacaoQuestaoRepository;
            _avaliacaoOpcaoService = avaliacaoOpcaoService;
        }

        public async Task<int> AdicionarAvaliacaoQuestaoAsync(string enunciado, int tipo, int id_avaliacao, List<AvaliacaoOpcao> opcoes)
        {
            var avaliacaoQuestao = new AvaliacaoQuestao()
            {
                Enunciado = enunciado,
                Tipo = tipo,
                IdAvaliacao = id_avaliacao,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now,
                Opcoes = opcoes
            };
            if (avaliacaoQuestao.Opcoes == null || avaliacaoQuestao.Opcoes.Count < 4)
                throw new Exception("Uma questão deve conter ao menos 4 opções!");
            else
            {
                if (avaliacaoQuestao.Tipo == 1)
                {
                    bool hasTrue = false;
                    foreach (AvaliacaoOpcao op in avaliacaoQuestao.Opcoes)
                    {
                        if (op.Resposta && !hasTrue)
                            hasTrue = true;
                        else if (op.Resposta && hasTrue)
                            throw new Exception("Questões múltipla escolha devem ter apenas uma opção verdadeira!");
                    }
                    if(!hasTrue)
                        throw new Exception("Questões múltipla escolha devem ter ao menos uma opção verdadeira!");
                }
                _avaliacaoQuestaoRepository.Adicionar(avaliacaoQuestao);
                await _avaliacaoQuestaoRepository.SaveChangesAsync();
                return avaliacaoQuestao.Id;
            }
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
