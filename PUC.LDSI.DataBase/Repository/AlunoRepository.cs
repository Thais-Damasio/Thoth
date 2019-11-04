using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;
using PUC.LDSI.DataBase.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PUC.LDSI.DataBase.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        private readonly AppDbContext _context;
        public AlunoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Aluno> ObterPorLogin(string login)
        {
            var retorno = await _context.Alunos
                .Where(x => x.Email == login)
                .FirstOrDefaultAsync();
            return retorno;
        }
    }
}

