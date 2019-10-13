using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;
using PUC.LDSI.DataBase.Context;

namespace PUC.LDSI.DataBase.Repository
{
    public class AvaliacaoOpcaoRepository : Repository<AvaliacaoOpcao>, IAvaliacaoOpcaoRepository
    {
        private readonly AppDbContext _context;
        public AvaliacaoOpcaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

