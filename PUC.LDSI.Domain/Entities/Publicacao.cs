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
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Valor { get; set; }
        [ForeignKey("Avaliacao")]
        public int IdAvaliacao { get; set; }
        [ForeignKey("Turma")]
        public int IdTurma { get; set; }

        //Relacionamentos
        public Avaliacao Avaliacao { get; set; }
        public Turma Turma { get; set; }

    }
}
