using System.ComponentModel.DataAnnotations;

namespace Compras.Models
{
    public class StateViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Departamento/Estados")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El compo {0} es Obligatorio. ")]
        public string Name { get; set; }


        public int CountryID { get; set; }
    }
}
