using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WhiskerHaven.UI.Models.Category;

namespace WhiskerHaven.UI.Models.Product
{
    public class ProductRequestModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(4, ErrorMessage = "El nombre debe contener minimo 4 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre debe contener menos de 50 caracteres")]
        public string Name { get; set; }

        [MinLength(4, ErrorMessage = "La descripción debe contener minimo 4 caracteres")]
        [MaxLength(120, ErrorMessage = "La descripción debe contener menos de 100 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Las unidades de stock es obligatorio")]
        [Range(0, 10000000, ErrorMessage = "El stock debe ser mayor a 0 y menor a 10000000")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(typeof(decimal), "1", "10000000", ErrorMessage = "El precio debe ser mayor a 1 y menor a 10000000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [NotNull]
        public int CategoryId { get; set; }
        public string UrlImage { get; set; }
    }
}
