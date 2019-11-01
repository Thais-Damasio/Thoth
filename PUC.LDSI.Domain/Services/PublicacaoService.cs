using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace PUC.LDSI.Domain.Services
{
    public class PublicacaoService : IPublicacaoService
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        public PublicacaoService(
            IPublicacaoRepository publicacaoRepository,
            IAvaliacaoRepository avaliacaoRepository
            )
        {
            _publicacaoRepository = publicacaoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<int> IncluirNovaPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, int idAvaliacao, int idTurma)
        {
            var publicacao = new Publicacao()
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                Valor = valor,
                IdAvaliacao = idAvaliacao,
                IdTurma = idTurma,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };
            var avaliacao = await _avaliacaoRepository.ObterComRelacoesAsync(publicacao.IdAvaliacao);
            if (avaliacao.Questoes == null || avaliacao.Questoes.Count <= 0)
                throw new Exception("Para ser publicada uma avaliação deve conter ao menos 1 questão!");

            _publicacaoRepository.Adicionar(publicacao);
            await _publicacaoRepository.SaveChangesAsync();
            return publicacao.Id;
        }

        public async Task IncluirNovaPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, int idAvaliacao, IEnumerable<int> idTurma)
        {
            var avaliacao = await _avaliacaoRepository.ObterComRelacoesAsync(idAvaliacao);
            if (avaliacao.Questoes == null || avaliacao.Questoes.Count <= 0)
                throw new Exception("Para ser publicada uma avaliação deve conter ao menos 1 questão!");

            foreach (int id in idTurma)
            {
                var publicacao = new Publicacao()
                {
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Valor = valor,
                    IdTurma = id,
                    IdAvaliacao = idAvaliacao,
                    CriadoEm = DateTime.Now,
                    AtualizadoEm = DateTime.Now
                };
                _publicacaoRepository.Adicionar(publicacao);
                await _publicacaoRepository.SaveChangesAsync();
            }
        }

        public async Task<int> AlterarPublicacaoAsync(int id, DateTime dataInicio, DateTime dataFim, int valor)
        {
            var publicacao = await _publicacaoRepository.ObterAsync(id);
            publicacao.DataInicio = dataInicio;
            publicacao.DataFim = dataFim;
            publicacao.Valor = valor;
            publicacao.AtualizadoEm = DateTime.Now;
            _publicacaoRepository.Modificar(publicacao);
            return await _publicacaoRepository.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            _publicacaoRepository.Remover(id);
            await _publicacaoRepository.SaveChangesAsync();
        }
    }
}
