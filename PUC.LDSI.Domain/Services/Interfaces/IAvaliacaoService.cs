using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<int> AdicionarAvaliacaoAsync(string materia, string descricao, int id_professor, int id_disciplina);
        Task<int> AlterarAvaliacaoAsync(int id, string materia, string descricao, int id_disciplina);
        Task ExcluirAsync(int id);
    }
}
