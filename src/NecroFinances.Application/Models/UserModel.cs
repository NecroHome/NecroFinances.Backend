using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NecroFinances.Application.Models
{
    [Table("Usuarios")]
    public class UserModel
    {
        [Key]
        [Column("ID")]
        public long ID { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("RefreshToken")]
        public string? RefreshToken { get; set; }

        [Column("RefreshTokenExpiryTime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
