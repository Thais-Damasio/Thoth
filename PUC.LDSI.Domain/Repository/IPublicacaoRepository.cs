using PUC.LDSI.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IPublicacaoRepository : IRepository<Publicacao>
    {
        Task<int> AdicionarPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, Turma turma, int id_avalicao);
        Task<Publicacao> ObterPublicacaoComAvaliacaoAsync(int idPublicacao);
        Task<int> ObterIdPublicacaoPorTurmaAvaliacaoAsync(int idTurma, int IdAvaliacao);
    }
}
