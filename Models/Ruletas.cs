using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasTenicas.Models
{
    //Table of roulets

    public class Ruletas
    {
        [Key]
        [Required]
        public int RuletaID {get; set;}
        public string Nombre { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
