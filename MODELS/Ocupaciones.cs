using System.ComponentModel.DataAnnotations;

public class Ocupaciones{

    [Key]
    public int OcupacionId{get;set;}
    [Required(ErrorMessage ="La descripción es requerida")]
    public string? Descripcion{get;set;}
    public float Salario{get;set;}
    
}