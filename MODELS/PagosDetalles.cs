using System.ComponentModel.DataAnnotations;

public class PagosDetalles{

    [Key]

    [Required(ErrorMessage = "El Id es requerido")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El pagoId es requerido")]
    public int PagoId { get; set; }

    [Required(ErrorMessage = "El prestamoId es requerido")]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "Ingrese un valorPagado")]
    public double ValorPagado { get; set; }
}