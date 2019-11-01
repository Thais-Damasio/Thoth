using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class PublicacaoService : IPublicacaoService
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private DateTime Now { get { return DateTime.Now; } }

        public PublicacaoService(IPublicacaoRepository publicacaoRepository, ITurmaRepository turmaRepository)
        {
            _publicacaoRepository = publicacaoRepository;
            _turmaRepository = turmaRepository;
        }

        public async Task<int> AdicionarPublicacaoAsync(DateTime dataInicio, DateTime dataFim, int valor, int id_turma, int id_avalicao)
        {
            var turma = await _turmaRepository.ObterAsync(id_turma);

            return await _publicacaoRepository.AdicionarPublicacaoAsync(dataInicio, dataFim, valor, turma, id_avalicao);
        }




        public async Task<bool> PublicacaoDisponivel(int idPublicacao)
        {
            var publicacao = await _publicacaoRepository.ObterAsync(idPublicacao);

            if (publicacao.DataInicio > Now)
                throw new Exception($"Avaliação poderá ser iniciada somente após {publicacao.DataInicio.ToString("dd/MM/yyy HH:mm")}");

            else if (publicacao.DataFim < Now)
                throw new Exception($"Avaliação encerrada em {publicacao.DataFim.ToString("dd/MM/yyy HH:mm")}");
            
            return false;
        }


    }
}
