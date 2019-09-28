using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class AvaliacaoOpcao : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Resposta { get; set; }
        [ForeignKey("AvaliacaoQuestao")]
        public int IdAvaliacaoQuestao { get; set; }

        //Relacionamentos
        public AvaliacaoQuestao Questao { get; set; }
       
    }
}
