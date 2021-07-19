using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebasTenicas.Data;
using PruebasTenicas.Entities;
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


        // GET: api/Ruletas
        // Function: See all roulettes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruletas>>> GetRoulette()
        {
            return await _context.Ruletas.ToListAsync();
        }

        // GET: api/Ruletas/{id}
        // Function: See specific roulette
        [HttpGet("{id}")]
        public async Task<ActionResult<Ruletas>> GetRoulette(RouletteEntities data)
        {
            var ruletas = await _context.Ruletas.FindAsync(data.RuletaID);

            if (ruletas == null)
            {
                return NotFound();
            }

            return ruletas;
        }

        // POST: api/Ruletas
        //Function : Create a new roulette
        [HttpPost]
        public async Task<ActionResult<Ruletas>> CreateRoulette(Ruletas data)
        {
            _context.Ruletas.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoulette), new { RuletaID = data.RuletaID }, data);
        }

        //Function: Activate roulettes
        [HttpPatch("OpenRoulette/{id}")]
        public async Task<IActionResult> OpenRoulette(int id, [FromQuery] Ruletas data)
        {
            if (id != null)
            {
                var Roulette = await _context.Ruletas.FindAsync(id);

                if (data.Estado != null)
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

        //validator id
        private bool RuletasExists(int id)
        {
            return _context.Ruletas.Any(e => e.RuletaID == id);
        }
    }
}
