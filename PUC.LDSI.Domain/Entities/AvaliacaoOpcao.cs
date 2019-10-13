using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class AvaliacaoOpcao : Entity
    {
        //Atributos
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Resposta { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Questão da Avaliação")]
        [ForeignKey("AvaliacaoQuestao")]
        public int IdAvaliacaoQuestao { get; set; }

        //Relacionamentos
        public AvaliacaoQuestao Questao { get; set; }
       
    }
}
