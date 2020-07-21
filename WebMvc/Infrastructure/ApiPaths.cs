using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.NewFolder
{
    public static class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}catalogtypes";
            }
            public static string GetAllBrands(string baseUri)
            {
                return $"{baseUri}catalogbrands";
            }
            public static string GetAllICatalogtems(string baseUri, int page, int take, int? brand, int? type)
            {
                var filterQs = string.Empty;
                if (brand.HasValue || type.HasValue)
                {
                    var brandQs = (brand.HasValue) ? brand.Value.ToString() : " ";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : " ";
                    filterQs = $"/type/{typeQs}/brand/{brandQs}";
                }
                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            internal static void GetAllCatalogtems(string baseUrl, int page, int size, int? brand, int? type)
            {
                throw new NotImplementedException();
            }
        }
    }
}
