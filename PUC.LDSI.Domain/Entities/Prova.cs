using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Prova : Entity
    {
        //Atributos
        [ForeignKey("Aluno")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Aluno")]
        public int IdAluno { get; set; }
        [ForeignKey("Avaliacao")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Avaliação")]
        public int IdAvaliacao { get; set; }
        [Display(Name = "Publicacação")]
        [ForeignKey("Publicacao")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int IdPublicacao { get; set; }

        //Relacionamentos
        public Aluno Aluno { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public Publicacao Publicacao { get; set; }
        public List<ProvaQuestao> Questoes { get; set; }


    }
}
