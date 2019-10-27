using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaRepository : Repository<Prova>, IProvaRepository
    {
        private readonly AppDbContext _context;
        private DateTime Now { get { return DateTime.Now; } }

        public ProvaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> AdicionarProvaAsync(int idAluno, int idAvaliacao)
        {   
            var prova = new Prova() { IdAluno = idAluno, IdAvaliacao = idAvaliacao };
            Adicionar(prova);
            await SaveChangesAsync();
            return prova.Id;
        }

        public async Task<Prova> ObterProvaComAlunoTurmaAsync(int IdProva)
        {
           return await _context.Provas
                                           .Include(x => x.Avaliacao)
                                           .Include(z => z.Aluno).ThenInclude(a => a.Turma)
                                           .Where(a => a.Id == IdProva)
                                           .FirstOrDefaultAsync();
        }
    }
}
