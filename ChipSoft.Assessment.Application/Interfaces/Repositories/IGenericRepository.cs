using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<Result<List<T>>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<Result<T>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<T>> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default);    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    IQueryable<T> Query();
}