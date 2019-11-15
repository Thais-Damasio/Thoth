using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class ProvaService : IProvaService
    {


        private readonly IProvaRepository _provaRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IProvaQuestaoRepository _provaQuestaoRepository;
        private readonly IProvaOpcaoRepository _provaOpcaoRepository;
        private readonly IPublicacaoRepository _publicacaoRepository;
        public ProvaService(
            IProvaRepository provaRepository,
            IPublicacaoRepository publicacaoRepository,
            IProvaOpcaoRepository provaOpcaoRepository,
            IProvaQuestaoRepository provaQuestaoRepository,
            IAvaliacaoRepository avaliacaoRepository
            )
        {
            _provaRepository = provaRepository;
            _publicacaoRepository = publicacaoRepository;
            _provaQuestaoRepository = provaQuestaoRepository;
            _provaOpcaoRepository = provaOpcaoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }


        public async Task<int> IncluirNovaProvaAsync(int idAvaliacao, int idPublicacao, int idAluno)
        {
            var avaliacao = await _avaliacaoRepository.ObterComRelacoesAsync(idAvaliacao);
            var publicacao = await _publicacaoRepository.ObterComRelacoesAsync(idPublicacao);

            if (publicacao.DataInicio >= DateTime.Now || publicacao.DataFim <= DateTime.Now)
                throw new Exception("Prova fora da data de realização!");
            if (publicacao.Turma.Alunos.Select(a => a.Id).Contains(idAluno))
                throw new Exception("Publicação não disponível para esse aluno!");

            var prova = new Prova()
            {
                IdAluno = idAluno,
                IdAvaliacao = idAvaliacao,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now

            };

            _provaRepository.Adicionar(prova);
            await _provaRepository.SaveChangesAsync();
            return prova.Id;
        }

        public async Task<int> IncluirNovaProvaQuestaoAsync(int idAvaliacaoQuestao, int idProva)
        {
            var provaQuestao = new ProvaQuestao()
            {
                AtualizadoEm = DateTime.Now,
                CriadoEm = DateTime.Now,
                IdAvaliacaoQuestao = idAvaliacaoQuestao,
                IdProva = idProva,
                Nota = 0
            };

            _provaQuestaoRepository.Adicionar(provaQuestao);
            await _provaQuestaoRepository.SaveChangesAsync();
            return provaQuestao.Id;
        }

        public async Task<int> IncluirNovaProvaOpcaoAsync(int idAvaliacaoOpcao, int idQuestaoProva, bool resposta = false)
        {
            var provaOpcao = new ProvaOpcao()
            {
                AtualizadoEm = DateTime.Now,
                CriadoEm = DateTime.Now,
                IdAvaliacaoOpcao = idAvaliacaoOpcao,
                IdQuestaoProva = idQuestaoProva,
                Resposta = resposta
            };

            _provaOpcaoRepository.Adicionar(provaOpcao);
            await _provaQuestaoRepository.SaveChangesAsync();
            return provaOpcao.Id;
        }

        public async Task AtualizarNotaProvaQuestaoAsync(int idQuestaoProva)
        {
            var provaQuestao = await _provaQuestaoRepository.ListarComRelacoesAsync(idQuestaoProva);
            double nota = 0;
            double valorOpcao = 0;

            if (provaQuestao.AvaliacaoQuestao.Tipo == 1)
            {
                valorOpcao = 1;
                foreach (var item in provaQuestao.Opcao)
                {
                    if (item.AvaliacaoOpcao.Resposta && item.Resposta == item.AvaliacaoOpcao.Resposta)
                    {
                        nota = 1;
                        break;
                    }
                }
            }
            else if (provaQuestao.AvaliacaoQuestao.Tipo == 2)
            {
                valorOpcao = 1 / provaQuestao.AvaliacaoQuestao.Opcoes.Count();

                foreach (var item in provaQuestao.Opcao)
                {
                    if (item.Resposta == item.AvaliacaoOpcao.Resposta)
                    {
                        nota += valorOpcao;
                    }
                }
            }

            provaQuestao.Nota = nota;
            provaQuestao.AtualizadoEm = DateTime.Now;
            _provaQuestaoRepository.Modificar(provaQuestao);
        }


        public async Task AtualizarProvaOpcaoAsync(int idProvaOpcao, bool resposta = false)
        {

            var provaOpcao = await _provaOpcaoRepository.ObterAsync(idProvaOpcao);
            provaOpcao.AtualizadoEm = DateTime.Now;
            provaOpcao.Resposta = resposta;

            _provaOpcaoRepository.Modificar(provaOpcao);
            await _provaQuestaoRepository.SaveChangesAsync();
        }

        public async Task<ProvaOpcao> ObterProvaOpcaoAsync(int idProvaQuestao)
        {
            var provaOpcao = await _provaOpcaoRepository.ObterComRelacionamentosAsync(idProvaQuestao);

            return provaOpcao;
        }

    }
}
