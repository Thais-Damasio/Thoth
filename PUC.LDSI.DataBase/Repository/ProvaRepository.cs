using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.DataBase.Context;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaRepository : Repository<Prova>, IProvaRepository
    {
        private readonly AppDbContext _context;
        public ProvaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
