using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IProvaService
    {

        Task<int> IncluirNovaProvaAsync(int idAvaliacao, int idPublicacao, int idAluno);

        Task<int> IncluirNovaProvaQuestaoAsync(int idAvaliacaoQuestao, int idProva);
        Task AtualizarNotaProvaQuestaoAsync(int idQuestaoProva);
        Task<int> IncluirNovaProvaOpcaoAsync(int idAvaliacaoOpcao, int idQuestaoProva, bool resposta = false);

        Task<ProvaOpcao> ObterProvaOpcaoAsync(int idProvaQuestao);
        Task AtualizarProvaOpcaoAsync(int idProvaOpcao, bool resposta = false);
    }
}
