using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IAlunoService
    {
        Task<int> IncluirNovoAlunoAsync(string email, string nome, int id_turma);
        Task<Aluno> BuscarPorEmail(string email);
    }
}
