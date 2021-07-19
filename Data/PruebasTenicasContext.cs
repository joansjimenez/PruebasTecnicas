using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebasTenicas.Models;

namespace PruebasTenicas.Data
{
    public class PruebasTenicasContext : DbContext
    {
        public PruebasTenicasContext (DbContextOptions<PruebasTenicasContext> options)
            : base(options)
        {
        }

        public DbSet<PruebasTenicas.Models.Ruletas> Ruletas { get; set; }
        public DbSet<PruebasTenicas.Models.Apuestas> Apuestas { get; set; }
        public DbSet<PruebasTenicas.Models.ApuestasNumeros> ApuestasNumeros { get; set; }
        public DbSet<PruebasTenicas.Models.Usuario> Usuario { get; set; }
    }
}
