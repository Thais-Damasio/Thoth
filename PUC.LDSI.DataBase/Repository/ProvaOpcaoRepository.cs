using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.DataBase.Context;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaOpcaoRepository : Repository<ProvaOpcao>, IProvaOpcaoRepository
    {
        private readonly AppDbContext _context;
        public ProvaOpcaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

