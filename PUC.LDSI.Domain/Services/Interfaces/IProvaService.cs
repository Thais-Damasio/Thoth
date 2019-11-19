using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.QueryResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IProvaService
    {
        Task<IEnumerable<ProvaQueryResult>> ObterProvasComRelacoes(string login);
        Task<ProvaQueryResult> ObterProvaAsync(int publicacaoId, string login);
        Task<int> SalvarProvaAsync(ProvaQueryResult prova, string login);
    }
}
