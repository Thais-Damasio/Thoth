using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Atributos de apoio
        [DataType(DataType.Date)]
        [Display(Name = "Criado em")]
        public DateTime CriadoEm { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Última Atualização")]
        public DateTime AtualizadoEm { get; set; }
    }
}
