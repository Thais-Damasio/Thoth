using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        public Task<int> IncluirNovaAvaliacaoAsync(Avaliacao avaliacao)
        {
            throw new NotImplementedException();
        }
    }
}
