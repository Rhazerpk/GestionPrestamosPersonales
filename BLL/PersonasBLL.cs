using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PersonasBLL{

    private Contexto _contexto;
    public PersonasBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int PersonaId)
    {
        return _contexto.Personas.Any(o => o.PersonaId == PersonaId);

    }

    private bool Insertar(Persona persona)
    {
        _contexto.Personas.Add(persona);
        return _contexto.SaveChanges() > 0;
    }
    public bool Editar(Persona persona)
    {
        if (!Existe(persona.PersonaId))
            return this.Insertar(persona);
        else
            return this.Modificar(persona);
    }

    private bool Modificar(Persona persona)
    {
        _contexto.Entry(persona).State = EntityState.Modified;
        return _contexto.SaveChanges() > 0;

    }

    public bool Guardar(Persona persona)
    {
        if(!Existe(persona.PersonaId))
            return this.Insertar(persona);
        else
            return this.Modificar(persona);
    }

    public bool Eliminar(Persona persona)
    {
        _contexto.Entry(persona).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }

    public Persona? Buscar(int personaID)
    {
        return _contexto.Personas
        .Where(o => o.PersonaId == personaID)
        .AsNoTracking()
        .SingleOrDefault();
    }

    public List<Persona> GetList(Expression<Func<Persona, bool>> Criterio)
    {
        return _contexto.Personas
        .AsNoTracking()
        .Where(Criterio)
        .ToList();
    }

        public List<Ocupaciones> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio)
    {
        return _contexto.Ocupaciones
        .AsNoTracking()
        .Where(Criterio)
        .ToList();
    }
    
}