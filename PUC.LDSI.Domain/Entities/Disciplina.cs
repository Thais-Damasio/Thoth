using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Disciplina : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        [StringLength(150)]
        public string Nome { get; set; }

        //Relacionamentos
        public List<Avaliacao> Avaliacoes { get; set; }

    }
}
