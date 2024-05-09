using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyModel;
using Microsoft.AspNetCore.Authorization;

namespace PlantGeneServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController(PlantsSourceContext context) : ControllerBase
    {
       

        // GET: api/Family  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamily()
        {
            return await context.Family.ToListAsync();
        }
        [HttpGet("FamiliesGenes/{id}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Gene>>> GetGenesByFamily(int id)
        {
            return await context.Gene.Where(c => c.FamilyId == id).ToListAsync();
        }

        // GET: api/Families/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(int id)
        {
            var family = await context.Family.FindAsync(id);

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

            context.Entry(family).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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
            context.Family.Add(family);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetFamily", new { id = family.FamilyId }, family);
        }

        // DELETE: api/Families/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamily(int id)
        {
            var family = await context.Family.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            context.Family.Remove(family);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamilyExists(int id)
        {
            return context.Family.Any(e => e.FamilyId == id);
        }
    }
}
