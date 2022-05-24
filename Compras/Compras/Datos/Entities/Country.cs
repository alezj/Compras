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


        public ICollection<State> States { get; set; }


        [Display(Name = "Departamentos/Estados")]
        public int StateNumber => States == null ? 0 : States.Count;
    }
}
