using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaQuestaoRepository : Repository<ProvaQuestao>, IProvaQuestaoRepository
    {
        private readonly AppDbContext _context;
        public ProvaQuestaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<ProvaQuestao> ListarComRelacoesAsync(int idProvaQuestao)
        {
            var provaQuestao = await _context.ProvaQuestoes
                                            .Where(e => e.Id == idProvaQuestao)
                                            .Include(a => a.AvaliacaoQuestao)
                                            .ThenInclude(a => a.Opcoes)
                                            .Include(a => a.Opcao)

                                            .FirstOrDefaultAsync();
            return provaQuestao;
        }
    }
}
