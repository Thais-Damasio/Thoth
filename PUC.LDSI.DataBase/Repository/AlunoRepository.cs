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
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        private readonly AppDbContext _context;
        public AlunoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        

        /// <summary>
        /// Obtendo todas as publicação para um aluno
        /// </summary>
        /// <param name="id">Id do aluno</param>
        /// <returns></returns>
        public async Task<List<Publicacao>> ObterPulicoesPorAlunoAsync(int id)
        {
            var publicacaos = await _context.Alunos
                                            .Include(x => x.Turma).ThenInclude(x => x.Publicacoes)
                                            .Where(x => x.Id == id)
                                            .Select(a => a.Turma.Publicacoes)
                                            .FirstOrDefaultAsync();

            return publicacaos;
        }
        /// <summary>
        ///   Login de usuarios alunos é a matricula
        /// </summary>
        /// <param name="login">Matricula do aluno usada no login</param>
        /// <returns></returns>
        public Aluno ObterPorLogin(int login)
        {
            var retorno = this.Consultar(x => x.Matricula == login).FirstOrDefault();
            return retorno;
        }
    }
}

