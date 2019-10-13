using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.DataBase.Context;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaQuestaoRepository : Repository<ProvaQuestao>, IProvaQuestaoRepository
    {
        private readonly AppDbContext _context;
        public ProvaQuestaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
