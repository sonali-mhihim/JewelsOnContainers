using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext context;
        private CatalogContext _context;

        public CatalogController(CatalogContext context)
        {
            _context = context;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery]int pageIndex = 0,
            [FromQuery]int pagesize =6)
       {
          var items = await _context.CatalogItems
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pagesize)
                .Take(pagesize)
                .ToListAsync();
            return Ok(items);
        }
    }
}