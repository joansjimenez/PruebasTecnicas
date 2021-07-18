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

        // GET: api/Ruletas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruletas>>> GetRuletas()
        {
            return await _context.Ruletas.ToListAsync();
        }

        // GET: api/Ruletas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ruletas>> GetRuletas(int id)
        {
            var ruletas = await _context.Ruletas.FindAsync(id);

            if (ruletas == null)
            {
                return NotFound();
            }

            return ruletas;
        }

        // PUT: api/Ruletas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuletas(int id, Ruletas ruletas)
        {
            if (id != ruletas.RuletaID)
            {
                return BadRequest();
            }

            _context.Entry(ruletas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuletasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ruletas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ruletas>> PostRuletas(Ruletas ruletas)
        {
            _context.Ruletas.Add(ruletas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuletas", new { id = ruletas.RuletaID }, ruletas);
        }

        // DELETE: api/Ruletas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuletas(int id)
        {
            var ruletas = await _context.Ruletas.FindAsync(id);
            if (ruletas == null)
            {
                return NotFound();
            }

            _context.Ruletas.Remove(ruletas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuletasExists(int id)
        {
            return _context.Ruletas.Any(e => e.RuletaID == id);
        }
    }
}
