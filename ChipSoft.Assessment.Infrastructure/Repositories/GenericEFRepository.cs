using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class GenericEFRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public GenericEFRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }

    protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> query) => query;

    public virtual async Task<Result<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result<T>();
        try
        {
            // Use ApplyIncludes to allow derived repositories to include navigations
            var query = ApplyIncludes(Query());
            var entity = await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                result.IsSuccess = false;
                result.Errors.Add($"Entity of type {typeof(T).Name} with id {id} not found.");
                return result;
            }

            result.IsSuccess = true;
            result.Data = entity;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("A database error occurred while retrieving the entity.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while retrieving the entity.");
            return result;
        }
    }

    public virtual async Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = new Result<List<T>>();
        try
        {
            var query = ApplyIncludes(Query());
            var list = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
            result.IsSuccess = true;
            result.Data = list;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("A database error occurred while retrieving the list of entities.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while retrieving the list of entities.");
            return result;
        }
    }

    public virtual async Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = new Result<T>();
        if (entity is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Entity cannot be null.");
            return result;
        }

        try
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            result.IsSuccess = true;
            result.Data = entity;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("Failed to save entity to the database.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while adding the entity.");
            return result;
        }
    }

    public virtual async Task<Result<List<T>>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var result = new Result<List<T>>();
        if (entities is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Entities collection cannot be null.");
            return result;
        }

        try
        {
            var list = entities.ToList();
            if (list.Count == 0)
            {
                result.IsSuccess = false;
                result.Errors.Add("Entities collection cannot be empty.");
                return result;
            }

            _dbSet.AddRange(list);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            result.IsSuccess = true;
            result.Data = list;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("Failed to save entities to the database.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while adding the entities.");
            return result;
        }
    }

    public virtual async Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = new Result<T>();
        if (entity is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Entity cannot be null.");
            return result;
        }

        try
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            result.IsSuccess = true;
            result.Data = entity;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("Failed to update entity in the database.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while updating the entity.");
            return result;
        }
    }

    public virtual async Task<Result<T>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result<T>();
        try
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                result.IsSuccess = false;
                result.Errors.Add($"Entity of type {typeof(T).Name} with id {id} not found.");
                return result;
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            result.IsSuccess = true;
            result.Data = entity;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("Failed to delete entity from the database.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while deleting the entity.");
            return result;
        }
    }

    public virtual async Task<Result<T>> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = new Result<T>();
        if (entity is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Entity cannot be null.");
            return result;
        }

        try
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            result.IsSuccess = true;
            result.Data = entity;
            return result;
        }
        catch (DbUpdateException)
        {
            result.IsSuccess = false;
            result.Errors.Add("Failed to delete entity from the database.");
            return result;
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Errors.Add("An unexpected error occurred while deleting the entity.");
            return result;
        }
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public virtual IQueryable<T> Query()
        => _dbSet.AsQueryable();
}