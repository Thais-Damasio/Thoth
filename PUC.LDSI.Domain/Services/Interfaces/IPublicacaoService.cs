using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IPublicacaoService
    {
        Task<bool> PublicacaoDisponivel(int idPublicacao);
    }
}
