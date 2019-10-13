using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Disciplina : Entity
    {
        //Atributos
        [StringLength(150, ErrorMessage = "O campo {0} deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        //Relacionamentos
        public List<Avaliacao> Avaliacoes { get; set; }

    }
}
