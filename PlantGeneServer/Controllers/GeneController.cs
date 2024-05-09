using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyModel;
using PlantGeneServer.DTO;
using Microsoft.AspNetCore.Authorization;



namespace PlantGeneServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneController(PlantsSourceContext context) : ControllerBase
    {
        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gene>>> GetGenes()
        {
            return await context.Gene.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetPlantsCost")]
        public async Task<ActionResult<IEnumerable<PlantsCost>>> GetPlantsCost()
        {
            IQueryable<PlantsCost> x = from c in context.Family
                                              select new PlantsCost
                                              {
                                                  Name = c.Name,
                                                  familyId = c.FamilyId,
                                                  cost = (int)c.Gene.Sum(t => t.cost)
                                              };
            return await x.ToListAsync();
        }
        [HttpGet("GetPlantsCost2")]
        public async Task<ActionResult<IEnumerable<PlantsCost>>> GetPlantsCost2()
        {
            IQueryable<PlantsCost> x = context.Family.Select(c =>
                                              new PlantsCost
                                              {
                                                  Name = c.Name,
                                                  familyId = c.FamilyId,
                                                  cost = (int)c.Gene.Sum(t => t.cost)
                                              });
            return await x.ToListAsync();
        }
    }
}