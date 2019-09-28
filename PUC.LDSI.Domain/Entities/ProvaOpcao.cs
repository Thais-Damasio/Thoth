using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class ProvaOpcao : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        [ForeignKey("ProvaQuestao")]
        public int IdQuestaoProva { get; set; }
        [ForeignKey("AvaliacaoOpcao")]
        public int IdAvaliacaoOpcao { get; set; }
        public bool Resposta { get; set; }

        //Relacionamentos
        public ProvaQuestao QuestaoProva { get; set; }
        public AvaliacaoOpcao AvaliacaoOpcao { get; set; }

    }
}
