using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{ 
    public class AvaliacaoQuestao : Entity
    {
        //Atributos
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Tipo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Enunciado { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Avaliação")]
        [ForeignKey("Avaliacao")]
        public int IdAvaliacao { get; set; }

        //Relacionamentos
        [Display(Name = "Avaliação")]
        public Avaliacao Avaliacao { get; set; }
        [Display(Name = "Opções")]
        public List<AvaliacaoOpcao> Opcoes { get; set; }
        
    }
}
