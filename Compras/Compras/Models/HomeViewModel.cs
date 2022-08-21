using Compras.Common;
using Compras.Datos.Entities;

namespace Compras.Models
{
    public class HomeViewModel
    {
        public PaginatedList<Product> Products { get; set; }

        public ICollection<Category> Categories { get; set; }

        public float Quantity { get; set; }
       
    }
}
