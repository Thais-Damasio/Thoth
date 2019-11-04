using PUC.LDSI.Domain.Services.Interfaces;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace PUC.LDSI.Domain.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<int> IncluirNovoAlunoAsync(string email, string nome, int id_turma)
        {
            var aluno = new Aluno() { Nome = nome, Email = email, IdTurma = id_turma };
            _alunoRepository.Adicionar(aluno);
            await _alunoRepository.SaveChangesAsync();
            return aluno.Id;
        }
    }
}
