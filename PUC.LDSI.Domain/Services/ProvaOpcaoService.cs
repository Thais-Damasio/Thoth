using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace PUC.LDSI.Domain.Services
{
    public class ProvaOpcaoService : IProvaOpcaoService
    {



        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IProvaQuestaoRepository _provaQuestaoRepository;
        private readonly IProvaRepository _provaRepository;
        private readonly IProvaOpcaoRepository _provaOpcaoRepository;
        private readonly IPublicacaoService _publicacaoService;

        public ProvaOpcaoService(IProvaRepository provaRepository,
                            IPublicacaoRepository publicacaoRepository,
                            IProvaQuestaoRepository provaQuestaoRepository,
                            IProvaOpcaoRepository provaOpcaoRepository,
                            IPublicacaoService publicacaoService)
        {
            _publicacaoRepository = publicacaoRepository;
            _provaQuestaoRepository = provaQuestaoRepository;
            _provaRepository = provaRepository;
            _provaRepository = provaRepository;
            _publicacaoService = publicacaoService;
            _provaOpcaoRepository = provaOpcaoRepository;
        }
        
    }
}
