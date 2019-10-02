using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        public Task<int> IncluirNovoAlunoAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Aluno ObterPorLogin(string login)
        {
            throw new NotImplementedException();
        }
    }
}
