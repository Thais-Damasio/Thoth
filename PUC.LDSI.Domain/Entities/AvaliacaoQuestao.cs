using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class AvaliacaoQuestao : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Enunciado { get; set; }
        public int IdAvaliacao { get; set; }
    
        //Relacionamentos
        public Avaliacao Avaliacao { get; set; }
        public List<AvaliacaoOpcao> Opcoes { get; set; }
        
    }
}
