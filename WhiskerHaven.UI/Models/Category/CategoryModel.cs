using System.ComponentModel.DataAnnotations;

namespace WhiskerHaven.UI.Models.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(4, ErrorMessage = "El nombre debe contener minimo 4 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre debe contener menos de 50 caracteres")]
        public string Name { get; set; }

        [MinLength(4, ErrorMessage = "La descripción debe contener minimo 4 caracteres")]
        [MaxLength(100, ErrorMessage = "La descripción debe contener menos de 100 caracteres")]
        public string? Description { get; set; }
    }
}
