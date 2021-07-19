using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebasTenicas.Data;
using PruebasTenicas.Models;

namespace PruebasTenicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApuestasController : ControllerBase
    {
        private readonly PruebasTenicasContext _context;

        public ApuestasController(PruebasTenicasContext context)
        {
            _context = context;
        }

        // Function: See specific roulette
        [HttpGet("{id}")]
        public async Task<ActionResult<Ruletas>> GetBet(Apuestas data)
        {
            var apuesta = await _context.Ruletas.FindAsync(data.ApuestaID);

            if (apuesta == null)
            {
                return NotFound();
            }

            return apuesta;
        }


        // Function: Create new bet
        [HttpPost("CreateBet")]
        public async Task<ActionResult<Apuestas>> CreateBet(Apuestas data)
        {
            if (data.CantidadApuesta > 10000 || data.CantidadApuesta < 0)
            {
                return StatusCode(400, "Ha ocurrido un error en el proceso, el valor de la apuesta es no cumple con los valores permitidos.");
            }
            else
            {
                var bet = await _context.ApuestasNumeros.FindAsync(data.ApuestaNumeroID);
                if (bet == null)
                {
                    return StatusCode(400, "El numero de la apuesta no es permitido.");
                }
                else
                {
                    var roulette = await _context.Ruletas.FindAsync(data.RuletaID);
                    var user = await _context.Usuario.FindAsync(data.UsuarioID);
                    if (roulette == null || roulette.Estado == false)
                    {
                        return StatusCode(400, "El numero de la apuesta no es permitido o la ruleta no cuenta con un estado activo.");
                    }
                    else if (user == null)
                    {
                        return StatusCode(400, "No se encuentra usuario que coincida con el enviado.");
                    }
                    else
                    {
                        if (user.Creditos > data.CantidadApuesta)
                        {
                            user.Creditos = user.Creditos - data.CantidadApuesta;
                            await _context.SaveChangesAsync();

                            _context.Apuestas.Add(data);
                            await _context.SaveChangesAsync();

                            return CreatedAtAction(nameof(GetBet), new { id = data.ApuestaID }, data);
                        }
                        else
                        {
                            return StatusCode(400, "El usuario no cuenta con creditos suficientes.");
                        }
                    }
                }
            }
        }

        private bool ApuestasExists(int id)
        {
            return _context.Apuestas.Any(e => e.ApuestaID == id);
        }
    }
}
