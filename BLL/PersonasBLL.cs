using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PersonasBLL{

    private Contexto _contexto;
    
    public PersonasBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int PersonaId)
    {
       return await _contexto.Personas.AnyAsync(o => o.PersonaId == PersonaId);

    }

     private async Task<bool> Insertar(Persona personas)
    {
    _contexto.Personas.Add(personas);
    int cantidad = await _contexto.SaveChangesAsync();
    return cantidad > 0;
    }

    private async Task<bool> Modificar(Persona personas)
    {
        _contexto.Entry(personas).State = EntityState.Modified;
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Persona personas)
    {
        if (!await Existe(personas.PersonaId))
            return await this.Insertar(personas);
        else
            return await this.Modificar(personas);
    }

    public async Task<bool> Eliminar(Persona personas)
    {
        _contexto.Entry(personas).State = EntityState.Deleted;
        return await _contexto.SaveChangesAsync() > 0;
    }


    public async Task<bool> Editar(Persona personas)
    {
        if(!await Existe(personas.PersonaId))
            return await this.Insertar(personas);
        else
            return await this.Modificar(personas);
    }

    public async Task<Persona?> Buscar(int personaId)
    {
        return await _contexto.Personas
        .Where(o => o.PersonaId == personaId)
        .AsNoTracking()
        .SingleOrDefaultAsync();

    }
    public async Task<List<Persona>> GetList(Expression<Func<Persona, bool>> Criterio)
    {
        return await _contexto.Personas
        .AsNoTracking()
        .Where(Criterio)
        .ToListAsync();
    }
    
}