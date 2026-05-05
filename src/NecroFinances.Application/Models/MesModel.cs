using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Models
{
    [Table("Meses")]
    public class MesModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Data")]
        public DateTime Data {  get; set; }

        [Column("DiasUteis")]
        public int DiasUteis { get; set; }

        [Column("HorasUteis")]
        public int HorasUteis { get; set; }

        [Column("HorasExtras")]
        public int HorasExtras { get; set; }

        [Column("UserID")]
        public long UserID { get; set; }
    }
}
