using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NecroFinances.Application.Models
{
    [Table("Settings")]
    public class SettingsModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Data")]
        public DateOnly Data { get; set; }

        [Column("ValorHora")]
        public double ValorHora { get; set; }

        [Column("SalarioMinimo")]
        public double SalarioMinimo { get; set; }

        [Column("PercentagemTaxaINSS")]
        public double PercentagemTaxaINSS { get; set; }

        [Column("PercentagemTaxaCooperativa")]
        public double PercentagemTaxaCooperativa { get; set; }

        [Column("ValorPlanoDental")]
        public double ValorPlanoDental { get; set; }

        [Column("ValorPlanoSaude")]
        public double ValorPlanoSaude { get; set; }

        [Column("DesafioGastos")]
        public double DesafioGastos { get; set; }

        [Column("UserID")]
        public long UserID { get; set; }
    }
}
