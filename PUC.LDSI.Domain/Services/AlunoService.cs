using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return  _alunoRepository.ObterTodos().ToList();
        }
        public async Task<Aluno> ObterAlunoAsync(int idAluno)
        {
            return await _alunoRepository.ObterAsync(idAluno);
        }

        public async Task<List<Publicacao>> ObterPulicacoesPorAlunoAsync(int idAluno)
        {
            return await _alunoRepository.ObterPulicacoesPorAlunoAsync(idAluno);
        }
    }
}
