using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class ProvaOpcao : Entity
    {
        //Atributos
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Questão")]
        [ForeignKey("ProvaQuestao")]
        public int IdQuestaoProva { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Disciplina")]
        [ForeignKey("AvaliacaoOpcao")]
        public int IdAvaliacaoOpcao { get; set; }
        public bool Resposta { get; set; }

        //Relacionamentos
        public ProvaQuestao ProvaQuestao { get; set; }
        public AvaliacaoOpcao AvaliacaoOpcao { get; set; }

    }
}
