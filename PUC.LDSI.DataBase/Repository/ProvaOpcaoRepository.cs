using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.DataBase.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaOpcaoRepository : Repository<ProvaOpcao>, IProvaOpcaoRepository
    {
        private readonly AppDbContext _context;
        public ProvaOpcaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<ProvaOpcao>> ObterPorQuestaoProvaAsync(int IdProvaQuestao)
        {
            var provaOpcao = _context.ProvaOpcoes
                .Where(x => x.IdQuestaoProva == IdProvaQuestao)
                .Include(x => x.ProvaQuestao)
                .Include(x => x.AvaliacaoOpcao)
                .ToList();
            return provaOpcao;
            
        }
        public async Task<ProvaOpcao> ObterComRelacionamentosAsync(int IdProvaQuestao)
        {
            var provaOpcao = await _context.ProvaOpcoes
                .Where(x => x.Id == IdProvaQuestao)
                .Include(x => x.ProvaQuestao)
                .Include(x => x.AvaliacaoOpcao)
                .FirstOrDefaultAsync();
            return provaOpcao;
        }
    }
    }

