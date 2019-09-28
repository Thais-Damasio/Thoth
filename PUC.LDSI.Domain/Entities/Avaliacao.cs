using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Avaliacao : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public string Materia { get; set; }
        public string Descricao { get; set; }
        [ForeignKey("Professor")]
        public int IdProfessor { get; set; }
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
