using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class Aluno : Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Atributos
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public string Senha { get; set; }
        public int IdTurma { get; set; }
        
        //Relacionamentos
        public Turma Turma { get; set; }
        public List<Prova> Provas { get; set; }

    }
}
