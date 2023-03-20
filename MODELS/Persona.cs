using System.ComponentModel.DataAnnotations;

public class Persona
{

    [Key]
    public int PersonaId {get; set;}
    [Required(ErrorMessage = "El campo nombre es requerida")]
    public string? Nombres {get; set;}
    [Required(ErrorMessage = "El número de teléfono es requerido")]
    public string? Telefono {get; set;}

    [Required(ErrorMessage = "El número de celular es requerido")]
    public string? Celular {get; set;}
    [Required(ErrorMessage = "El email es requerido")]
    public string? Email {get; set;}
    [Required(ErrorMessage = "La dirección es requerido")]
    public string? Direccion {get; set;}
    public DateOnly? FechaNacimiento {get; set;}
    public int OcupacionId {get; set;}
    [Required(ErrorMessage = "El balance es requerido")]
    public double Balance {get;set;}

}
