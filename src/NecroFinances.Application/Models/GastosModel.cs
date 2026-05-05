using NecroFinances.Application.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Models
{
    [Table("Gastos")]
    public class GastosModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Serie")]
        public long Serie { get; set; }

        [Column("DataGasto")]
        public DateTime DataGasto { get; set; }

        [Column("TipoGasto")]
        public IndicadorTipoGasto TipoGasto { get; set; }

        [Column("Valor")]
        public double Valor { get; set; }

        [Column("Icone")]
        public string Icone { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("Parcela")]
        public int Parcela { get; set; }

        [Column("TotalParcelas")]
        public int TotalParcelas { get; set; }

        [Column("UserID")]
        public long UserID { get; set; }
    }
}
