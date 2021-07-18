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
        public int RuletaID {get; set;}
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
