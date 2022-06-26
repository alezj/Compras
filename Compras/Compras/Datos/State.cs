using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Compras.Datos.Entities
{
    public class State
    {
        public int ID { get; set; }

        [Display(Name = "Departamento/Estados")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El compo {0} es Obligatorio. ")]
        public string Name { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }

        public ICollection <City> Cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}

