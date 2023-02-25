using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PrestamosBLL{

    private Contexto _contexto;
    public PrestamosBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int PrestamoId)
    {
        return _contexto.Prestamos.Any(o => o.PrestamoId == PrestamoId);
    }

   private bool Insertar(Prestamo prestamos)
    {
        _contexto.Prestamos.Add(prestamos);
        return _contexto.SaveChanges() > 0;
    }
    public bool Editar(Prestamo prestamos)
    {
        if (!Existe(prestamos.PrestamoId))
            return this.Insertar(prestamos);
        else
            return this.Modificar(prestamos);
    }
    private bool Modificar(Prestamo prestamos)
    {
        _contexto.Entry(prestamos).State = EntityState.Modified;
        return _contexto.SaveChanges() > 0;

    }

    public bool Guardar(Prestamo prestamos)
    {
        if(!Existe(prestamos.PersonaId))
            return this.Insertar(prestamos);
        else
            return this.Modificar(prestamos);
    }

    public bool Eliminar(Prestamo prestamos)
    {
        _contexto.Entry(prestamos).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }

    public Prestamo? Buscar(int PrestamoId)
    {
        return _contexto.Prestamos
        .Where(o => o.PersonaId == PrestamoId)
        .AsNoTracking()
        .SingleOrDefault();
    }
    public List<Prestamo> GetList(Expression<Func<Prestamo, bool>> Criterio)
    {
        return _contexto.Prestamos
        .AsNoTracking()
        .Where(Criterio)
        .ToList();
    }

     public List<Persona> GetPersonas(Expression<Func<Persona, bool>> Criterio)
    {
        return _contexto.Personas
        .AsNoTracking()
        .Where(Criterio)
        .ToList();
    }
}