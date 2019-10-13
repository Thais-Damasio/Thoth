using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Professor : Entity
    {
        //Atributos
        [StringLength(150, ErrorMessage = "O campo {0} deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [StringLength(150, ErrorMessage = "O campo {0} deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Matrícula")]
        public int Matricula { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        //Relacionamentos
        public List<Avaliacao> Avaliacoes { get; set; }




    }
}
