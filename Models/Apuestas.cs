using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasTenicas.Models
{
    public class Apuestas
    {
        [Key]
        [Required]
        public int ApuestaID { get; set; }
        [Required]
        public bool EstadoApuesta { get; set; }
        [Required]
        public int RuletaID { get; set; }
        [Required]
        public int ApuestaNumeroID { get; set; }
        [Required]
        public int UsuarioID { get; set; }
        [Required]
        public double CantidadApuesta { get; set; }
        [Required]
        public double PremioApuesta { get; set; }
    }
}
