using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class OcupacionesBLL{

    private Contexto _contexto;
    
    public OcupacionesBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int ocupacionId){

        return await _contexto.Ocupaciones.AnyAsync(o => o.OcupacionId == ocupacionId);

    }
    private async Task<bool> Insertar(Ocupaciones ocupacion){

        _contexto.Ocupaciones.Add(ocupacion);
        return await _contexto.SaveChangesAsync() > 0;

    }
    public async Task<bool> Modificar(Ocupaciones ocupaciones){
        _contexto.Entry(ocupaciones).State = EntityState.Modified;
        return await _contexto.SaveChangesAsync() > 0;

    }
    public async Task<bool> Guardar(Ocupaciones ocupacion){

        if (! await Existe(ocupacion.OcupacionId))
            return await this.Insertar(ocupacion);
        else
            return await this.Modificar(ocupacion);

    }
    public async Task<bool> Editar(Ocupaciones ocupacion)
    {
        if (!await Existe(ocupacion.OcupacionId))
            return await this.Insertar(ocupacion);
        else
            return await this.Modificar(ocupacion);
    }

    public async Task<bool> Eliminar(Ocupaciones ocupacion){

        try
        {
             _contexto.Entry(ocupacion).State = EntityState.Deleted;
            return await _contexto.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            throw;
        }
       

    }

    public async Task<Ocupaciones?> Buscar(int ocupacionId){

        return await  _contexto.Ocupaciones
        .Where(o=> o.OcupacionId == ocupacionId)
        .AsNoTracking()
        .SingleOrDefaultAsync();     
    }
    public async Task<List<Ocupaciones>> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio){

        return await _contexto.Ocupaciones
        .AsNoTracking()
        .Where(Criterio)
        .ToListAsync();

    }
}