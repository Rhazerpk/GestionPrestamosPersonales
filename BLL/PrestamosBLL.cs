using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PrestamosBLL{

    private Contexto _contexto;
    public PrestamosBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int prestamosId)
    {
        return await _contexto.Prestamos.AnyAsync(o => o.PrestamoId == prestamosId);
    }

    public async Task<bool> Guardar(Prestamo prestamos)
    {
        if (!await Existe(prestamos.PrestamoId))
            return await this.Insertar(prestamos);
        else
            return await this.Modificar(prestamos);
    }

    public async Task<bool> Eliminar(Prestamo prestamos)
    {
    var personas = await _contexto.Personas.FindAsync(prestamos.PersonaId);
    personas.Balance -= prestamos.Monto;

    _contexto.Entry(prestamos).State = EntityState.Deleted;
    return await _contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Insertar(Prestamo prestamos)
    {
    await _contexto.Prestamos.AddAsync(prestamos);
    
    var persona = await _contexto.Personas.FindAsync(prestamos.PersonaId);
    persona.Balance += prestamos.Monto;
    
    int cantidad = await _contexto.SaveChangesAsync();
    
    return cantidad > 0;

    }

    public async Task<bool> Editar(Prestamo prestamos)
    {
        if (!await Existe(prestamos.PrestamoId))
            return await this.Insertar(prestamos);
        else
            return await this.Modificar(prestamos);
    }

    public async Task<bool> Modificar(Prestamo prestamoActual)
    {
        var prestamoAnterior = await _contexto.Prestamos
        .Where(p => p.PrestamoId == prestamoActual.PrestamoId)
        .AsNoTracking()
        .SingleOrDefaultAsync();

        var personaAnterior = await _contexto.Personas.FindAsync(prestamoActual.PersonaId);
        personaAnterior.Balance -= prestamoActual.Monto;

        _contexto.Entry(prestamoActual).State = EntityState.Modified;
        
        var persona = await _contexto.Personas.FindAsync(prestamoActual.PersonaId);
        persona.Balance += prestamoActual.Monto;

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<Prestamo?> Buscar(int PrestamoId)
    {
        return await _contexto.Prestamos
        .Where(o => o.PrestamoId == PrestamoId)
        .AsNoTracking()
        .SingleOrDefaultAsync();
    }
    public async Task<List<Prestamo>> GetList(Expression<Func<Prestamo, bool>> Criterio)
    {
        return await _contexto.Prestamos
        .AsNoTracking()
        .Where(Criterio)
        .ToListAsync();
    }
}