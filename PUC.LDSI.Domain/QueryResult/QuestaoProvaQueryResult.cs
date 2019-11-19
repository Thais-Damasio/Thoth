using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PUC.LDSI.Domain.QueryResult
{
    public class QuestaoProvaQueryResult
    {
        public int ProvaId { get; set; }
        public int QuestaoId { get; set; }
        public string Enunciado { get; set; }
        public int Tipo { get; set; }
        [IsTrue(ErrorMessage = "Nem todas as questões foram preenchidas")]
        public bool Completa { get; set; }
        public List<OpcaoProvaQueryResult> Opcoes { get; set; }
    }
    public class IsTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("can only be used on boolean properties.");

            return (bool)value;
        }
    }
}
