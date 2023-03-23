using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PagosBLL{

    private Contexto _contexto;
    
    public PagosBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int pagosId)
    {
        return await _contexto.Pagos.AnyAsync(o => o.PagoId == pagosId);
    }
    private async Task <bool> Insertar(Pago pago)
    {
        _contexto.Pagos.Add(pago);
        
        foreach (var item in pago.PagosDetalles)
        {
            var prestamo = await _contexto.Prestamos.FindAsync(item.PrestamoId);
            prestamo!.Balance -= item.ValorPagado;

        }
        var persona =  _contexto.Personas.Find(pago.PersonaId);
        persona!.Balance -= pago.Monto;

        var insertados = _contexto.SaveChanges();

        return insertados > 0;

    }
    private async Task<bool> Modificar(Pago pagoActual)
    {
        var pagoAnterior = _contexto.Pagos
        .Where(p => p.PagoId == pagoActual.PagoId)
        .AsNoTracking()
        .SingleOrDefault();

        var Persona = _contexto.Personas.Find(pagoAnterior!.PersonaId);
        Persona!.Balance += pagoAnterior.Monto;

        foreach (var item in pagoAnterior.PagosDetalles)
        {
            var prestamos =  _contexto.Prestamos.Find(item.PrestamoId);
            prestamos!.Balance += item.ValorPagado;

        }

        await _contexto.Database.ExecuteSqlRawAsync($"Delete from PagosDetalles Where PagoId = {pagoActual.PagoId}");

        foreach (var item in pagoActual.PagosDetalles)
        {
            _contexto.Entry(item).State = EntityState.Added;

            var prestamo = _contexto.Prestamos.Find(item.PrestamoId);
            prestamo!.Balance -= item.ValorPagado;
        }

        Persona.Balance -= pagoActual.Monto;

        _contexto.Entry(pagoActual).State = EntityState.Modified;

        _contexto.Entry(pagoActual).State = EntityState.Detached;

        return _contexto.SaveChanges() > 0;

    }
    public async Task<bool> Guardar(Pago pagos)
    {
        if (!await Existe(pagos.PagoId))
            return await this.Insertar(pagos);
        else
            return await this.Modificar(pagos);
    }

    public async Task<bool> Eliminar(Pago pagos)
    {
        var persona = await _contexto.Personas.FindAsync(pagos.PersonaId);
        persona!.Balance += pagos.Monto;

        foreach (var count in pagos.PagosDetalles)
        {
            var prestamo = await _contexto.Prestamos.FindAsync(count.PrestamoId);
            prestamo!.Balance += count .ValorPagado;
        }

        _contexto.Entry(pagos).State = EntityState.Deleted;

        return await _contexto.SaveChangesAsync() > 0;
    }
    public async Task<Pago?> Buscar(int pagoId)
    {
        return await _contexto.Pagos
        .Where(o => o.PagoId == pagoId)
        .Include(o => o.PagosDetalles)
        .AsNoTracking()
        .SingleOrDefaultAsync();
    }

    public async Task<List<Pago>> GetList(Expression<Func<Pago, bool>> Criterio)
    {
        return await _contexto.Pagos
        .Where(Criterio)
        .AsNoTracking()
        .ToListAsync();
    }
}