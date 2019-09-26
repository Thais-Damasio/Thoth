using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class ProvaQuestao : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public int IdAvaliacaoQuestao { get; set; }
        public double Nota { get; set; }

        //Relacionamentos
        public AvaliacaoQuestao AvaliacaoQuestao { get; set; }
        public Prova Prova { get; set; }
        public List<ProvaOpcao> Opcao { get; set; }

    }
}
