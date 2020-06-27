using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using ProductCatalogAPI.ViewModels;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery]int pageIndex = 0,
            [FromQuery]int pageSize = 6)
        {
            var itemsCount = await _context.CatalogItems.LongCountAsync();

            var items = await _context.CatalogItems
                  .OrderBy(c => c.Name)
                  .Skip(pageIndex * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
            items = ChangePictureUrl(items);
            var model = new PaginatedItemsViewModel<CatalogItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount,
                Data = items,
            };
            return Ok(model);
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(item =>
           item.PictureUrl = item.PictureUrl.Replace(
                             "http://externalcatalogbaseurltobereplaced",
                              _config["ExternalCatalogBaseUrl"]));
            return items;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var types = await _context.CatalogTypes.ToListAsync();
            return Ok(types);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CatalogBrands()
        {
            var brands = await _context.CatalogBrands.ToListAsync();
            return Ok(brands);
        }
        [HttpGet("[action]/type/{catalogTypeId}/brand/{ catalogBrandId}")]
        public async Task<IActionResult> Items(
            int? catalogTypeId,
            int? catalogBrandId,
           [FromQuery] int pageIndex = 0,
           [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<CatalogItem>)_context.CatalogItems;
            if (catalogTypeId.HasValue)
            {
                query = query.Where(c => c.CatalogTypeId == catalogTypeId);
            }
            if (catalogBrandId.HasValue)
            {
                query = query.Where(c => c.CatalogBrandId == catalogBrandId);
            }
            var itemsCount = query.LongCountAsync();

            var items = await query
                  .OrderBy(c => c.Name)
                  .Skip(pageIndex * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
            items = ChangePictureUrl(items);
            var model = new PaginatedItemsViewModel<CatalogItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount.Result,
                Data = items,
            };
            return Ok(model);

        }
    }
}