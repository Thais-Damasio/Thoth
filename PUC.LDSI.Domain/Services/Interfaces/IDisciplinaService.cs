using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IDisciplinaService
    {
        Task<int> AdicionarDisciplinaAsync(string nome);
        Task<int> AlterarDisciplinaAsync(int id, string nome);
        Task ExcluirAsync(int id);
    }
}
