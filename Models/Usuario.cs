using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasTenicas.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int UsuarioID { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public double Creditos { get; set; }

    }
}
