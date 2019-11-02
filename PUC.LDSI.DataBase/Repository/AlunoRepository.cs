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
        public async Task<List<Publicacao>> ObterPulicacoesPorAlunoAsync(string email)
        {
            var publicacaos = await _context.Alunos
                                            .Include(x => x.Turma).ThenInclude(x => x.Publicacoes)
                                            .Where(x => x.Email == email)
                                            .Select(a => a.Turma.Publicacoes)
                                            .FirstOrDefaultAsync();

            return publicacaos;
        }
        /// <summary>
        /// Obtendo todas as publicação para um aluno
        /// </summary>
        /// <param name="id">Id do aluno</param>
        /// <returns></returns>
        public async Task<Aluno> ObterAlunoDetailsAsync(int id)
        {
            var aluno = await _context.Alunos
                                            .Include(x => x.Turma).ThenInclude(x => x.Publicacoes)
                                            .Include(x => x.Provas)
                                            .Where(x => x.Id == id)
                                            .FirstOrDefaultAsync();

            return aluno;
        }

        public async Task<Aluno> ObterAlunoAsync(int id)
        {
            var aluno = await this.ObterAsync(id);

            return aluno;
        }

        public async Task<Aluno> ObterAlunoComProvasAsync(int id)
        {
            var alunos = await _context.Alunos.Where(x=>x.Id == id)
                                            .Include(x => x.Provas)
                                            .FirstOrDefaultAsync();

            return alunos;
        }

        public async Task<List<Aluno>> ObterAlunos()
        {
            var alunos = this.ObterTodos();

            return alunos.ToList();
        }
        /// <summary>
        ///   Login de usuarios alunos é a Email
        /// </summary>
        /// <param name="Email">Email do aluno usada no login</param>
        /// <returns></returns>
        public Aluno ObterPorLogin(string Email)
        {
            var retorno = this.Consultar(x => x.Email == Email).FirstOrDefault();
            return retorno;
        }
    }
}

