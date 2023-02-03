using System.ComponentModel.DataAnnotations;

public class Prestamo{

    [Key]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "Favor de ingresar la fecha de inicio.")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Favor de ingresar la fecha de vencimiento")]
    public DateTime Vence { get; set; }

    [Required(ErrorMessage = "Favor de ingresar el monto")]
    public double Monto { get; set; }

    [Required(ErrorMessage = "Favor selecccionar una pesona")]
    public int PersonaId { get; set; }

    [Required(ErrorMessage = "Favor de ingresar el concepto")]
    public string? Concepto { get; set; }
    public double Balance { get; set; }
}