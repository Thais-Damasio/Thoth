using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Turma : Entity
    {
        //Atributos
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        //Relacionamentos
        public List<Aluno> Alunos { get; set; }
        public List<Publicacao> Publicacoes { get; set; }

    }
}
