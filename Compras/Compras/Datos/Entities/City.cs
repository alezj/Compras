using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Compras.Datos.Entities
{
    public class City
    {
        public int ID { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El compo {0} es Obligatorio. ")]
        public string Name { get; set; }

        [JsonIgnore]
        public State State { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
