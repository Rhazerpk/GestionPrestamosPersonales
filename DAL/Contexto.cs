using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext{

    #nullable disable
    public DbSet<Ocupaciones> Ocupaciones {get; set;}
    public DbSet<Persona> Personas {get; set;}
    public DbSet<Prestamo> Prestamos {get; set;}
    public DbSet<Pago> Pagos {get; set;}
    public DbSet<PagosDetalles> PagosDetalles {get; set;}
    public Contexto(DbContextOptions <Contexto> options): base(options)
    {
    }

}
