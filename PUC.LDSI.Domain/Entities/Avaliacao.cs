using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Avaliacao : Entity
    {
        //Atributos
        [Display(Name = "Matéria")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Materia { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Professor")]
        [ForeignKey("Professor")]
        public int IdProfessor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Disciplina")]
        [ForeignKey("Disciplina")]
        public int IdDisciplina { get; set; }

        //Relacionamentos
        public Professor Professor { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<Publicacao> Publicacoes { get; set; }
        public List<AvaliacaoQuestao> Questoes { get; set; }
        public List<Prova> Provas { get; set; }


    }
}
