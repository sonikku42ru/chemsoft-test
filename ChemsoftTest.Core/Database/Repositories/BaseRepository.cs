using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChemsoftTest.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChemsoftTest.Core.Database.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : BaseEntity, new()
{
    protected DbContext Context { get; }

    public bool Connected => Context.Database.CanConnect();

    public BaseRepository(DbContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> GetAll() => 
        Context.Set<TEntity>().AsNoTracking();

    public async Task<TEntity> GetByIdAsync(int id) => 
        await Context.Set<TEntity>().FindAsync(id);

    public async Task<TEntity> AddOrUpdateAsync(TEntity entity, int? id = null)
    {
        if (id == null)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        else
        {
            var modifiedEntity = await GetByIdAsync(id.Value);
            if (modifiedEntity != null)
                Context.Entry(modifiedEntity).State = EntityState.Detached;
            Context.Entry(entity).State = EntityState.Modified;

        }
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task AddOrUpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        var list = entities.ToList();
        await Task.Run(() => Context
            .UpdateRange(list
                .Where(i => !i.IsNew)));
        await Context.SaveChangesAsync();
        await AddRangeAsync(list
            .Where(i => i.IsNew));
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var list = entities.ToList();
        await Context.Set<TEntity>().AddRangeAsync(list);
        await Context.SaveChangesAsync();
        return list;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        var result = await Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
        var result = await Context.SaveChangesAsync();
        return result > 0;
    }
}