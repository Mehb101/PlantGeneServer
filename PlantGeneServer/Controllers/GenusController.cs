using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyModel;

namespace PlantGeneServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenusController(PlantsSourceContext _context) : ControllerBase
    {

        // GET: api/Genus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genu>>> GetGenus()
        {
            return await _context.Genus.ToListAsync();
        }

        // GET: api/Genus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genu>> GetGenu(int id)
        {
            var genu = await _context.Genus.FindAsync(id);

            if (genu == null)
            {
                return NotFound();
            }

            return genu;
        }

        // PUT: api/Genus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenu(int id, Genu genu)
        {
            if (id != genu.GenusId)
            {
                return BadRequest();
            }

            _context.Entry(genu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenuExists(id))
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

        // POST: api/Genus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genu>> PostGenu(Genu genu)
        {
            _context.Genus.Add(genu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenu", new { id = genu.GenusId }, genu);
        }

        // DELETE: api/Genus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenu(int id)
        {
            var genu = await _context.Genus.FindAsync(id);
            if (genu == null)
            {
                return NotFound();
            }

            _context.Genus.Remove(genu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenuExists(int id)
        {
            return _context.Genus.Any(e => e.GenusId == id);
        }
    }
}
