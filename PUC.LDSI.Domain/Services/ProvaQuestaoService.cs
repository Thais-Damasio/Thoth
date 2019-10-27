using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class ProvaQuestaoService : IProvaQuestaoService
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IProvaQuestaoRepository _provaQuestaoRepository;
        private readonly IProvaRepository _provaRepository;
        private readonly IPublicacaoService _publicacaoService;
      
        public ProvaQuestaoService(IProvaRepository provaRepository,
                            IPublicacaoRepository publicacaoRepository,
                            IProvaQuestaoRepository provaQuestaoRepository,
                            IPublicacaoService publicacaoService)
        {
            _publicacaoRepository = publicacaoRepository;
            _provaQuestaoRepository = provaQuestaoRepository;
            _provaRepository = provaRepository;
            _publicacaoService = publicacaoService;
        }

        public async Task<int> AdicionarProvaQuestaoAsync(int IdAvaliacaoQuestao, int IdProva)
        {
            var prova = await _provaRepository.ObterProvaComAlunoTurmaAsync(IdProva);

            var idPublicacao = await _publicacaoRepository.ObterIdPublicacaoPorTurmaAvaliacaoAsync(prova.Aluno.Turma.Id, prova.Avaliacao.Id);

            if (await _publicacaoService.PublicacaoDisponivel(idPublicacao))
                return await _provaQuestaoRepository.AdicionarProvaQuestaoAsync(IdAvaliacaoQuestao, IdProva);
            else
                return 0;


        }
    }
}
