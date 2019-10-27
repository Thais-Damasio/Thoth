using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PUC.LDSI.DataBase.Repository
{
    public class PublicacaoRepository : Repository<Publicacao>, IPublicacaoRepository
    {
        private readonly AppDbContext _context;
        public PublicacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> AdicionarPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, Turma turma, int id_avalicao)
        {
            var publicacao = new Publicacao()
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                Valor = valor,
                IdAvaliacao = id_avalicao,
            };
            this.Adicionar(publicacao);
            await this.SaveChangesAsync();
            return publicacao.Id;
        }

        public async Task<Publicacao> ObterPublicacaoComAvaliacaoAsync(int idPublicacao)
        {


            var publicacao = await _context.Publicacoes
                                              .Include(x => x.Avaliacao)
                                              .Where(x => x.Id == idPublicacao)
                                              .FirstOrDefaultAsync();

            return publicacao;
        }


        public async Task<int> ObterIdPublicacaoPorTurmaAvaliacaoAsync(int idTurma, int IdAvaliacao)
        {


            return await _context.Publicacoes.Include(a=>a.Turma)
                                           .Where(p => p.IdAvaliacao == IdAvaliacao && p.Turma.Id == idTurma)
                                           .Select(a=>a.Id)
                                           .FirstOrDefaultAsync();

         
        }


    }
}
