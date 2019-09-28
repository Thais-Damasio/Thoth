using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
   public class Publicacao : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Valor { get; set; }
        [ForeignKey("Avaliacao")]
        public int IdAvaliacao { get; set; }

        //Relacionamentos
        public Avaliacao Avaliacao { get; set; }
        public Turma Turma { get; set; }

    }
}
