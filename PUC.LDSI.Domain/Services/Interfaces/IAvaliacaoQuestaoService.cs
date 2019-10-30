using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IAvaliacaoQuestaoService
    {
        Task<int> AdicionarAvaliacaoQuestaoAsync(string enunciado, int tipo, int id_avaliacao, List<AvaliacaoOpcao> opcoes);
        Task<int> AlterarAvaliacaoQuestaoAsync(int id, string enunciado, int tipo, int id_avaliacao);
        Task ExcluirAsync(int id);
    }
}
