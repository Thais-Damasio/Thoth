using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PUC.LDSI.Domain.QueryResult
{
    public class ProvaQueryResult
    {
        public int AvaliacaoId { get; set; }
        public int PublicacaoId { get; set; }
        [Display(Name = "Nota Obtida")]
        public double NotaObtida { get; set; }
        [Display(Name = "Realizado Em")]
        public DateTime? Criado_Em { get; set; }

        public Publicacao Publicacao { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public List<QuestaoProvaQueryResult> Questoes { get; set; }
    }
}
