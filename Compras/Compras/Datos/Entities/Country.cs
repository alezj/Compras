using System.ComponentModel.DataAnnotations;

namespace Compras.Datos.Entities
{
    public class Country
    {
        public int ID { get; set; }
        
        [Display(Name = "Pais")]
        [MaxLength(50, ErrorMessage ="El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El compo {0} es Obligatorio. ")]
        public string Name { get; set; }
    }
}
