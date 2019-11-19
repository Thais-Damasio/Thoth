using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class ProvaQuestao : Entity
    {
        //Atributos
        [ForeignKey("AvaliacaoQuestao")]
        public int IdAvaliacaoQuestao { get; set; }
        [ForeignKey("Prova")]
        public int IdProva { get; set; }
        public double Nota { get; set; }

        //Relacionamentos
        public AvaliacaoQuestao AvaliacaoQuestao { get; set; }
        public Prova Prova { get; set; }
        public List<ProvaOpcao> Opcoes { get; set; }

    }
}
