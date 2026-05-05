using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NecroFinances.Application.Models
{
    [Table("Investimentos")]
    public class InvestimentoModel
    {
        [Key]
        public long Id { get; set; }
        public long PatrimonioId { get; set; }
        public PatrimonioModel Patrimonio { get; set; }
        public string NomeInvestimento { get; set; }
        public double ValorInvestimento { get; set; }

    }
}
