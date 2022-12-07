using System.ComponentModel.DataAnnotations;

namespace Compras.Datos.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Nombre Foto")]
        public String ImageName { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageName == String.Empty 
            ? $"http://localhost/images/noImageProduct.jpg"
            //:$"http://localhost/Imagenes/adidas_barracuda.jpg";
          : $"http://localhost/Imagenes/products/{ImageName}";
    }

}
