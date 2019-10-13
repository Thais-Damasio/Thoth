using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }
        public async Task<int> AdicionarTurmaAsync(string descricao)
        {
            var turma = new Turma() { Nome = descricao, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now };
            _turmaRepository.Adicionar(turma);
            await _turmaRepository.SaveChangesAsync();
            return turma.Id;
        }
        public async Task<int> AlterarTurmaAsync(int id, string descricao)
        {
            var turma = await _turmaRepository.ObterAsync(id);
            turma.Nome = descricao;
            turma.AtualizadoEm = DateTime.Now;
            _turmaRepository.Modificar(turma);
            return await _turmaRepository.SaveChangesAsync();
        }
        public async Task ExcluirAsync(int id)
        {
            var turma = await _turmaRepository.ObterComAlunosAsync(id);
            if (turma.Alunos?.Count > 0)
                throw new Exception("Não é possível excluir uma turma que já possui atividades!");
            _turmaRepository.Remover(id);
            await _turmaRepository.SaveChangesAsync();
        }
    }

}

