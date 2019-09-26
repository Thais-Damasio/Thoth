using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Prova : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public int IdAvaliacao { get; set; }

        //Relacionamentos
        public Aluno Aluno { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public List<ProvaQuestao> Questoes { get; set; }


    }
}
