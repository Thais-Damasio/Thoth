using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IPublicacaoService
    {
        Task<int> IncluirNovaPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, int idAvaliacao, int idTurma);
        Task IncluirNovaPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, int idAvaliacao, IEnumerable<int> idTurma);
        Task<int> AlterarPublicacaoAsync(int id, DateTime dataInicio, DateTime dataFim, int valor);
        Task ExcluirAsync(int id);
    }
}
