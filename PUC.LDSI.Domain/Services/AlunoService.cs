using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class AlunoService : IAlunoService
    {

        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }
        public async Task<int> IncluirNovoAlunoAsync(Aluno aluno)
        {
            _alunoRepository.Adicionar(aluno);
            await _alunoRepository.SaveChangesAsync();
            return aluno.Id;
        }

        public List<Aluno> ObterAlunosAsync()
        {
            return _alunoRepository.ObterTodos().ToList();
        }
        public async Task<Aluno> ObterAlunoAsync(int idAluno)
        {
            return await _alunoRepository.ObterAsync(idAluno);
        }

        public async Task<List<Publicacao>> ObterPulicacoesPorAlunoAsync(string usuario)
        {
            return await _alunoRepository.ObterPulicacoesPorAlunoAsync(int.Parse(usuario));
        }
        /// <summary>
        /// Obtendo todas as publicação para um aluno
        /// </summary>
        /// <param name="id">Id do aluno</param>
        /// <returns></returns>
        public async Task<Aluno> ObterAlunoDetailsAsync(int id)
        {
            var aluno = await _alunoRepository.ObterAlunoDetailsAsync(id);
            return aluno;
        }
        public async Task<int> AdicionarAlunoAsync(Aluno aluno)
        {
            _alunoRepository.Adicionar(aluno);
            await _alunoRepository.SaveChangesAsync();
            return aluno.Id;
        }
        public async Task<int> AlterarAlunoAsync(Aluno aluno)
        {
            aluno.AtualizadoEm = DateTime.Now;
            _alunoRepository.Modificar(aluno);
            return await _alunoRepository.SaveChangesAsync();
        }
        public async Task ExcluirAsync(int id)
        {
            var Aluno = await _alunoRepository.ObterAlunoComProvasAsync(id);
            if (Aluno.Provas?.Count > 0)
                throw new Exception("Não é possível excluir uma Aluno que já possui atividades!");
            _alunoRepository.Remover(id);
            await _alunoRepository.SaveChangesAsync();
        }

    }
}
