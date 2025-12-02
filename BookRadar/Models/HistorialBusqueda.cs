namespace BookRadar.Models
{
    public class HistorialBusqueda
    {
        public int Id { get; set; }
        public string Autor { get; set; } =null!;
        public string Titulo { get; set; } =null!;
        public int? AnioPublicacion { get; set; }
        public string? Editorial { get; set; }
        public DateTime FechaBusqueda { get; set; }
    }
}
