using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyModel;
using CsvHelper.Configuration;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using CsvHelper;
using PlantGeneServer.Data;
using Microsoft.AspNetCore.Identity;

namespace PlantGeneServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(PlantsSourceContext db, IHostEnvironment environment,
        UserManager<PlantGeneUser> userManager) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/Plants.csv");

        [HttpPost("Users")]
        public async Task<ActionResult> SeedUsers()
        {
            (string name, string email) = ("user1", "comp584@csun.edu");
            PlantGeneUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";
            }
            _ = await userManager.CreateAsync(user, "P@ssw0rd!")
                ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Gene")]

        public async Task<ActionResult<Gene>> SeedGene()
        {
            Dictionary<string, Family> family = await db.Family//.AsNoTracking()
            .ToDictionaryAsync(c => c.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int geneCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<Plants>? records = csv.GetRecords<Plants>();
                foreach (Plants record in records)
                {
                    if (!family.TryGetValue(record.family, out Family? value))
                    {
                        Console.WriteLine($"Not found Family for {record.gene}");
                        return NotFound(record);
                    }

                    if (!record.cost.HasValue  || string.IsNullOrEmpty(record.gene))
                    {
                        Console.WriteLine($"Skipping {record.gene}");
                        continue;
                    }
                    Gene Gene = new()
                    {
                        Name = record.gene,
                        Size = record.size,
                        Charecteristic = record.charecteristic,
                        cost = (int)record.cost.Value,
                        FamilyId = value.FamilyId
                    };
                    db.Gene.Add(Gene);
                    geneCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(geneCount);
        }

        [HttpPost("Family")]
        public async Task<ActionResult<Gene>> SeedFamily()
        {
            // create a lookup dictionary containing all the countries already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Family> familiesByName = db.Family
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<Plants> records = csv.GetRecords<Plants>().ToList();
            foreach (Plants record in records)
            {
                if (familiesByName.ContainsKey(record.family))
                {
                    continue;
                }

                Family family = new()
                {
                    Name = record.family,
                    //Iso2 = record.iso2,
                    //Iso3 = record.iso3
                };
                await db.Family.AddAsync(family);
                familiesByName.Add(record.family, family);
            }

            await db.SaveChangesAsync();

            return new JsonResult(familiesByName.Count);
        }

    }
}