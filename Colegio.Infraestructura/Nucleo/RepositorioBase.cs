using Microsoft.EntityFrameworkCore;
using Colegio.Dominio.Nucleo;
using Colegio.Infraestructura.Contexto;

namespace Colegio.Infraestructura.Nucleo;

public class RepositorioBase<T>(ContextoColegio context) : IRepositorio<T> where T : EntidadBase
{
    protected readonly ContextoColegio Context = context;
    protected readonly DbSet<T> Set = context.Set<T>();

    public virtual async Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default) => await Set.AsNoTracking().ToListAsync(cancellationToken);
    public virtual Task<T?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) => Set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    public virtual async Task<T> AgregarAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Add(entity);
        await Set.AddAsync(entity, cancellationToken);
        return entity;
    }
    public virtual async Task<bool> ActualizarAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (!await Set.AnyAsync(x => x.Id == entity.Id, cancellationToken)) return false;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public virtual async Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Set.FindAsync([id], cancellationToken);
        if (entity is null) return false;
        Set.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
