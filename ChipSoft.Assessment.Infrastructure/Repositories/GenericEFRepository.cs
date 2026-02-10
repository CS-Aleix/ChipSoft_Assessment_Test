using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class GenericEFRepository
{
    protected readonly AppDbContext _dbContext;

    public GenericEFRepository(AppDbContext dbContext)
    {

    }

    //public virtual async Task<Result<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    //{
    //    return new Result<T>();
    //}

    //public virtual async Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default)
    //{
    //    return new Result<List<T>>();
    //}

    //public virtual async Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
    //{
    //    return new Result<T>();
    //}

    //public virtual async Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    //{
    //    return new Result<T>();
    //}

    //public virtual async Task<Result<T>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    //{
    //    return new Result<T>();
    //}

    //public virtual async Task<Result<T>> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    //{
    //    return new Result<T>();
    //}
}
