using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Pago{

    [Key]

    [Required(ErrorMessage = "El pagoId es requerido")]
    public int PagoId {get; set;}
     [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha {get; set;}
    [Required(ErrorMessage = "La personaId es requerido")]
    public int PersonaId {get; set;}
    [Required(ErrorMessage = "El concepto es requerido")]
    public string? Concepto {get; set;}
    [Required(ErrorMessage = "El monto es requerido")]
    public double Monto {get; set;}
    
    [ForeignKey("PagoId")]
    public virtual List<PagosDetalles> PagosDetalles { get; set; } = new List<PagosDetalles>();
    

}