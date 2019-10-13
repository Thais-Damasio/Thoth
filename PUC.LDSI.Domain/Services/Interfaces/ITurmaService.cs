using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface ITurmaService
    {
        Task<int> AdicionarTurmaAsync(string descricao);
        Task<int> AlterarTurmaAsync(int id, string descricao);
        Task ExcluirAsync(int id);
    }
}
