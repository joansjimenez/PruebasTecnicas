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
    public class RuletasController : ControllerBase
    {
        private readonly PruebasTenicasContext _context;

        public RuletasController(PruebasTenicasContext context)
        {
            _context = context;
        }

        // Function: See all roulettes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruletas>>> GetRoulette()
        {
            return await _context.Ruletas.ToListAsync();
        }

        // Function: See specific roulette
        [HttpGet("{id}")]
        public async Task<ActionResult<Ruletas>> GetRoulette(Ruletas data)
        {
            var ruletas = await _context.Ruletas.FindAsync(data.RuletaID);

            if (ruletas == null)
            {
                return NotFound();
            }

            return ruletas;
        }

        //Function : Create a new roulette
        [HttpPost]
        public async Task<ActionResult<Ruletas>> CreateRoulette(Ruletas data)
        {
            _context.Ruletas.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoulette), new { id = data.RuletaID }, data);
        }

        //Function: Activate roulettes
        [HttpPatch("OpenRoulette")]
        public async Task<IActionResult> OpenRoulette( Ruletas data)
        {
            if (data.RuletaID != null)
            {
                var Roulette = await _context.Ruletas.FindAsync(data.RuletaID);

                if (data.Estado == true && Roulette.Estado == false)
                {
                    Roulette.Estado = data.Estado;
                    await _context.SaveChangesAsync();
                    return StatusCode(201, Roulette);
                }
                else
                {
                    return StatusCode(400, "Ha ocurrido un error en el proceso.");
                }

            }
            else
            {
                return StatusCode(400, "Ha ocurrido un error en el proceso.");
            }
        }

        //Function: Close active roulettes and give prizes
        [HttpPatch("CloseRoulette")]
        public async Task<IActionResult> CloseRoulette(Ruletas data)
        {
            //select winner game
            System.Random rnd = new System.Random();
            var WinnerNumber = rnd.Next(0, 37);

            //validate winner
            if (data.RuletaID != null)
            {
                var roulette = await _context.Ruletas.FindAsync(data.RuletaID);
                if (roulette.Estado == true)
                {
                    var AllBets = await _context.Apuestas.Where(x => x.RuletaID == data.RuletaID).AsNoTracking().ToListAsync();
                    if (AllBets.Count > 0)
                    {
                        for (int i = 0; i < AllBets.Count; i++)
                        {
                            var user = await _context.Usuario.FindAsync(AllBets[i].UsuarioID);
                            var BetResult = await _context.Apuestas.FindAsync(AllBets[i].ApuestaID);

                            //winner with normal number

                            if (AllBets[i].ApuestaNumeroID == WinnerNumber)
                            {
                                var profit = BetResult.CantidadApuesta * 5;
                                BetResult.EstadoApuesta = false;
                                BetResult.PremioApuesta = BetResult.CantidadApuesta * 5;
                                user.Creditos = user.Creditos + profit;

                                await _context.SaveChangesAsync();
                            }
                            // winner with red color
                            else if ((WinnerNumber % 2) == 0)
                            {
                                if (AllBets[i].ApuestaNumeroID == 38)
                                {
                                    var profit = BetResult.CantidadApuesta * 1.8;
                                    BetResult.EstadoApuesta = false;
                                    BetResult.PremioApuesta = BetResult.CantidadApuesta * 1.8;
                                    user.Creditos = user.Creditos + profit;
                                    await _context.SaveChangesAsync();
                                }
                            }
                            // winner with black color
                            else
                            {
                                if (AllBets[i].ApuestaNumeroID == 39)
                                {
                                    var profit = BetResult.CantidadApuesta * 1.8;
                                    BetResult.EstadoApuesta = false;
                                    BetResult.PremioApuesta = BetResult.CantidadApuesta * 1.8;
                                    user.Creditos = user.Creditos + profit;
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                        var ExitData = await _context.Apuestas.Where(y => y.RuletaID == data.RuletaID).AsNoTracking().ToListAsync();
                        return StatusCode(201, ExitData);
                    }
                    else
                    {
                        return StatusCode(400, "La ruleta no contaba con apuestas activas.");
                    }
                }
                else
                {
                    return StatusCode(400, "La ruleta no se encuentra activa.");
                }
            }
            else
            {
                return StatusCode(400, "Se requiere una ruleta");
            }
        }

        //validator id
        private bool RuletasExists(int id)
        {
            return _context.Ruletas.Any(e => e.RuletaID == id);
        }
    }
}
