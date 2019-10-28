using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Aluno : Entity
    {
        //Atributos
        [StringLength(150, ErrorMessage = "O campo {0} deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [ForeignKey("Turma")]
        public int IdTurma { get; set; }
        
        //Relacionamentos
        public Turma Turma { get; set; }
        public List<Prova> Provas { get; set; }

    }
}
