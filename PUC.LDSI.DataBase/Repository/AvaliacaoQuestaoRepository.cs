using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class AvaliacaoQuestaoRepository : IAvaliacaoQuestaoRepository
    {
        public Task<int> IncluirNovoQuestaoAvaliacaoAsync(AvaliacaoQuestao avQuestao)
        {
            throw new NotImplementedException();
        }
    }
}
