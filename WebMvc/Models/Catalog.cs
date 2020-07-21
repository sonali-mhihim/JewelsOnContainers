using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class Catalog
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public long Count { get; set; }
        public List<CatalogItem> Data { get; set; }
    }
}
