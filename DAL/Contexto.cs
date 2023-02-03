using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext{

    public DbSet<Ocupaciones> Ocupaciones {get; set;}
    public DbSet<Persona> Personas {get; set;}
    public DbSet<Prestamo> Prestamos {get; set;}
    public Contexto(DbContextOptions <Contexto> options): base(options)
    {
    }

}
