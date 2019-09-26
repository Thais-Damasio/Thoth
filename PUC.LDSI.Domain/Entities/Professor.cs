using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Professor : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Matricula { get; set; }
        public string Senha { get; set; }

        //Relacionamentos
        public List<Avaliacao> Avaliacoes { get; set; }




    }
}
