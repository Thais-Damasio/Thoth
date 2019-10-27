using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaQuestaoRepository : Repository<ProvaQuestao>, IProvaQuestaoRepository
    {
        private readonly AppDbContext _context;
        private DateTime Now { get { return DateTime.Now; } }
        public ProvaQuestaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<int> AdicionarProvaQuestaoAsync(int IdAvaliacaoQuestao, int IdProva)
        {
            var provaQuestao = new ProvaQuestao() { IdAvaliacaoQuestao = IdAvaliacaoQuestao, IdProva = IdProva };
            this.Adicionar(provaQuestao);
            await this.SaveChangesAsync();
            return provaQuestao.Id;
        }

    }
}
