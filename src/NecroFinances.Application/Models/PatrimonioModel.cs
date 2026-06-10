using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NecroFinances.Application.Models
{
    [Table("Patrimonios")]
    public class PatrimonioModel
    {
        [Key]
        public long Id { get; set; }

        public DateOnly Data { get; set; }

        public long UserID { get; set; }

        public List<PropriedadeModel> Propriedades { get; set; }
        public List<InvestimentoModel> Investimentos { get; set; }
        public List<FinanciamentoModel> Financiamentos { get; set; }
    }
}
