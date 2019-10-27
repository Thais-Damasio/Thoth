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
    public class ProvaOpcaoRepository : Repository<ProvaOpcao>, IProvaOpcaoRepository
    {
        private readonly AppDbContext _context;
        private DateTime Now { get { return DateTime.Now; } }
        public ProvaOpcaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}

