using System.ComponentModel.DataAnnotations;

namespace BookRadar.Models
{
    public class BuscarAutorViewModel
    {
        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El autor debe tener entre 2 y 50 caracteres.")]
        public string Autor { get; set; } = string.Empty;

    }
}
