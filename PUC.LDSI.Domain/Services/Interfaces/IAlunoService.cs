using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IAlunoService
    {

        Task<int> IncluirNovoAlunoAsync(Aluno aluno);
        Task<Aluno> ObterAlunoAsync(int idAluno);
        List<Aluno> ObterAlunosAsync();
        Task<List<Publicacao>> ObterPulicacoesPorAlunoAsync(string usuario);
        Task<Aluno> ObterAlunoDetailsAsync(int id);
        Task<int> AlterarAlunoAsync(Aluno aluno);
        Task ExcluirAsync(int id);
    }
}
