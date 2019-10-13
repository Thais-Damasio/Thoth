using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IAvaliacaoOpcaoService
    {
        Task<int> AdicionarAvaliacaoOpcaoAsync(string descricao, bool resposta, int id_avaliacao_opcao);
        Task<int> AlterarAvaliacaoOpcaoAsync(int id, string descricao, bool resposta, int id_avaliacao_opcao);
        Task ExcluirAsync(int id);
    }
}
