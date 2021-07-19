using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasTenicas.Models
{
    public class ApuestasNumeros
    {
        [Key]
        [Required]
        public int ApuestaNumeroID { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
