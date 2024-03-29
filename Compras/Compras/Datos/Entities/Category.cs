﻿using System.ComponentModel.DataAnnotations;

namespace Compras.Datos.Entities
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "Categorias")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El compo {0} es Obligatorio. ")]
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        [Display(Name = "# Productos")]
        public int ProductsNumber => ProductCategories == null ? 0 : ProductCategories.Count();

    }
}

