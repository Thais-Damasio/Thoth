﻿using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;
using PUC.LDSI.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace PUC.LDSI.DataBase.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        private readonly AppDbContext _context;
        public AvaliacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Avaliacao>> ListarComRelacoesAsync()
        {
            var avaliacoes = await _context.Avaliacoes
           .Include(x => x.Professor)
           .Include(d => d.Disciplina)
           .ToListAsync();
            return avaliacoes;
        }

        public async Task<Avaliacao> ObterComPublicacaoAsync(int id)
        {
            var avaliacao = await _context.Avaliacoes
           .Include(x => x.Publicacoes)
           .Where(x => x.Id == id).FirstOrDefaultAsync();
            return avaliacao;
        }
    }
}

