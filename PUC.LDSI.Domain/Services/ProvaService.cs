using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;
namespace PUC.LDSI.Domain.Services
{
    public class ProvaService : IProvaService
    {

        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IProvaRepository _provaRepository;
        private readonly IPublicacaoService _publicacaoService;

        public ProvaService(IProvaRepository provaRepository,
                            IPublicacaoRepository publicacaoRepository,
                            ITurmaRepository turmaRepository,
                            IPublicacaoService publicacaoService)
        {
            _publicacaoRepository = publicacaoRepository;
            _turmaRepository = turmaRepository;
            _provaRepository = provaRepository;
            _publicacaoService = publicacaoService;
        }

        public async Task<int> AdicionarProvaAsync(int idAluno, int idPublicacao)
        {
            if (await _publicacaoService.PublicacaoDisponivel(idPublicacao))
            {
                var publicacao = await _publicacaoRepository.ObterPublicacaoComAvaliacaoAsync(idPublicacao);

                return await _provaRepository.AdicionarProvaAsync(idAluno, publicacao.Avaliacao.Id);
            }
            else
                return 0;
        }
    }
}
