using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;
using PUC.LDSI.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace PUC.LDSI.DataBase.Repository
{
    public class AvaliacaoQuestaoRepository : Repository<AvaliacaoQuestao>, IAvaliacaoQuestaoRepository
    {
        private readonly AppDbContext _context;
        public AvaliacaoQuestaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<AvaliacaoQuestao>> ObterComRelacoesAsync(int idAvalicao)
        {
            var questao = await  _context.AvaliacaoQuestoes
           .Include(x => x.Opcoes)
           .Include(x => x.Avaliacao)
           .Where(x => x.IdAvaliacao == idAvalicao).ToListAsync() ;
            return questao;
        }
    }
}

