using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
   public class Publicacao : Entity
    {
        //Atributos
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int? Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Avaliação")]
        [ForeignKey("Avaliacao")]
        public int IdAvaliacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Turma")]
        [ForeignKey("Turma")]
        public int IdTurma { get; set; }

        //Relacionamentos
        [Display(Name = "Avaliação")]
        public Avaliacao Avaliacao { get; set; }
        public Turma Turma { get; set; }

    }
}
