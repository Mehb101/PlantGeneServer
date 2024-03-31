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
    public class FamiliesController : ControllerBase
    {
        private readonly PlantsSourceContext _context;

        public FamiliesController(PlantsSourceContext context)
        {
            _context = context;
        }

        // GET: api/Families
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamilies()
        {
            return await _context.Families.ToListAsync();
        }

        // GET: api/Families/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(int id)
        {
            var family = await _context.Families.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return family;
        }

        // PUT: api/Families/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamily(int id, Family family)
        {
            if (id != family.FamilyId)
            {
                return BadRequest();
            }

            _context.Entry(family).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(id))
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

        // POST: api/Families
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Family>> PostFamily(Family family)
        {
            _context.Families.Add(family);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamily", new { id = family.FamilyId }, family);
        }

        // DELETE: api/Families/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamily(int id)
        {
            var family = await _context.Families.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            _context.Families.Remove(family);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamilyExists(int id)
        {
            return _context.Families.Any(e => e.FamilyId == id);
        }
    }
}
