using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.QueryResult;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class ProvaService : IProvaService
    {
        private readonly IProvaRepository _provaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IProvaQuestaoRepository _provaQuestaoRepository;
        private readonly IProvaOpcaoRepository _provaOpcaoRepository;
        private readonly IPublicacaoRepository _publicacaoRepository;
        public ProvaService(
            IProvaRepository provaRepository,
            IPublicacaoRepository publicacaoRepository,
            IProvaOpcaoRepository provaOpcaoRepository,
            IProvaQuestaoRepository provaQuestaoRepository,
            IAlunoRepository alunoRepository,
            IAvaliacaoRepository avaliacaoRepository
            )
        {
            _provaRepository = provaRepository;
            _publicacaoRepository = publicacaoRepository;
            _provaQuestaoRepository = provaQuestaoRepository;
            _alunoRepository = alunoRepository;
            _provaOpcaoRepository = provaOpcaoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }
        public async Task<IEnumerable<ProvaQueryResult>> ObterProvasComRelacoes(string login)
        {
            var aluno = await _alunoRepository.ObterPorLogin(login);

            if (aluno == null) throw new Exception("O aluno não foi localizado!");
            
            IEnumerable<Prova> provas = await _provaRepository.ObterProvasComRelacoesAsync(aluno.Id);
            List<ProvaQueryResult> provaList = new List<ProvaQueryResult>();

            foreach(Prova prova in provas)
            {
                var retorno = new ProvaQueryResult
                {
                    Avaliacao = prova.Avaliacao,
                    Publicacao = prova.Publicacao,
                    NotaObtida = prova.Publicacao.Valor.Value /prova.Questoes.Count * prova.Questoes.Sum(x => x.Nota)
                };

                provaList.Add(retorno);
            }

            return provaList;
        }

        public async Task<ProvaQueryResult> ObterProvaAsync(int publicacaoId, string login)
        {
            var aluno = _alunoRepository.ObterPorLogin(login);

            if (aluno == null) throw new Exception("O aluno não foi localizado!");

            var publicacao = await _publicacaoRepository.ObterComRelacoesAsync(publicacaoId);

            if (publicacao == null) throw new Exception("A publicacao não foi localizada!");

            var avaliacaoCompleta = await _avaliacaoRepository.ObterComRelacoesAsync(publicacao.IdAvaliacao);

            var provaCompleta = await _provaRepository.ObterProvaDoAlunoAsync(publicacao.IdAvaliacao, aluno.Id);

            var retorno = new ProvaQueryResult()
            {
                AvaliacaoId = publicacao.IdAvaliacao,
                PublicacaoId = publicacao.Id,
                Avaliacao = publicacao.Avaliacao,
                Publicacao = publicacao,
                Questoes = avaliacaoCompleta.Questoes.Select(x => new QuestaoProvaQueryResult()
                {
                    QuestaoId = x.Id,
                    Enunciado = x.Enunciado,
                    Tipo = x.Tipo,
                    Completa = false,
                    Opcoes = x.Opcoes.Select(y => new OpcaoProvaQueryResult()
                    {
                        OpcaoAvaliacaoId = y.Id,
                        QuestaoId = y.IdAvaliacaoQuestao,
                        Descricao = y.Descricao,
                        Resposta = false
                    }).ToList()
                }).ToList()
            };

            retorno.Questoes.SelectMany(x => x.Opcoes).ToList().ForEach(x => {
                x.Resposta = provaCompleta?.Questoes?
                    .SelectMany(y => y.Opcoes)
                    .FirstOrDefault(y => y.IdAvaliacaoOpcao == x.OpcaoAvaliacaoId)?.Resposta ?? false;
            });

            return retorno;
        }

        public async Task<int> SalvarProvaAsync(ProvaQueryResult prova, string login)
        {
            var aluno = await _alunoRepository.ObterPorLogin(login);

            if (aluno == null) throw new Exception("O aluno não foi localizado!");

            var publicacao = await _publicacaoRepository.ObterComRelacoesAsync(prova.PublicacaoId);

            if (publicacao == null) throw new Exception("A publicacao não foi localizada!");

            if (publicacao.DataInicio >= DateTime.Now || publicacao.DataFim <= DateTime.Now)
                throw new Exception("Prova fora da data de realização!");
            
            if (!publicacao.Turma.Alunos.Select(a => a.Id).ToList().Contains(aluno.Id))
                throw new Exception("Publicação não disponível para esse aluno!");

            var Novaprova = new Prova()
            {
                IdAluno = aluno.Id,
                IdPublicacao = publicacao.Id,
                IdAvaliacao = publicacao.IdAvaliacao,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now,
                Questoes = prova.Questoes.Select(q => new ProvaQuestao 
                {
                    IdAvaliacaoQuestao = q.QuestaoId,
                    IdProva = 0,
                    Nota = 0,
                    AtualizadoEm = DateTime.Now,
                    CriadoEm = DateTime.Now,
                }).ToList()
            };

            Avaliacao av = publicacao.Avaliacao;
            for(int i = 0; i < av.Questoes.Count; i++)
            {
                Novaprova.Questoes[i].Opcoes = new List<ProvaOpcao>();
                if (av.Questoes[i].Tipo == 1)
                {
                    for (int j = 0; j < av.Questoes[i].Opcoes.Count; j++)
                    {
                        AvaliacaoOpcao op = av.Questoes[i].Opcoes[j];
                        OpcaoProvaQueryResult opResposta = prova.Questoes[i].Opcoes[j];

                        Novaprova.Questoes[i].Opcoes.Add(new ProvaOpcao{
                            IdAvaliacaoOpcao = opResposta.OpcaoAvaliacaoId,
                            Resposta = opResposta.Resposta
                        });
                        if (op.Resposta == opResposta.Resposta)
                        {
                            Novaprova.Questoes[i].Nota = 1;
                        }
                    }
                }
                else
                {
                    double valorOpcao = 1.0 / av.Questoes[i].Opcoes.Count();
                    for (int j = 0; j < av.Questoes[i].Opcoes.Count; j++)
                    {
                        AvaliacaoOpcao op = av.Questoes[i].Opcoes[j];
                        OpcaoProvaQueryResult opResposta = prova.Questoes[i].Opcoes[j];

                        Novaprova.Questoes[i].Opcoes.Add(new ProvaOpcao
                        {
                            IdAvaliacaoOpcao = opResposta.OpcaoAvaliacaoId,
                            Resposta = opResposta.Resposta
                        });
                        if (op.Resposta == opResposta.Resposta)
                        {
                            Novaprova.Questoes[i].Nota += valorOpcao;
                        }
                    }
                }
            }

            _provaRepository.Adicionar(Novaprova);
            await _provaRepository.SaveChangesAsync();
            return Novaprova.Id;
        }

        ///

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
                foreach (var item in provaQuestao.Opcoes)
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

                foreach (var item in provaQuestao.Opcoes)
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
