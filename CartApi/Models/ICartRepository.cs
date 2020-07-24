using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        Task<Cart> UpdateCartAsync(Cart basket);
        Task<bool> DeleteCartAsync(string id);
        IEnumerable<string> GetUsers();
    }
}
