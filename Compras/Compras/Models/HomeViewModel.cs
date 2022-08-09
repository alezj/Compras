using Compras.Datos.Entities;

namespace Compras.Models
{
    public class HomeViewModel
    {
        public ICollection<Product> Products { get; set; }

        public float Quantity { get; set; }

    }
}
