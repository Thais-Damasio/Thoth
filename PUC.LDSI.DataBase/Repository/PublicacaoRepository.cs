using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.DataBase.Repository
{
    public class PublicacaoRepository : Repository<Publicacao>, IPublicacaoRepository
    {
        private readonly AppDbContext _context;
        public PublicacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
