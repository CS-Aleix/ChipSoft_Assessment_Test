using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class PatientEFRepository : IPatientRepository
{
    protected AppDbContext _dbContext;
    protected DbSet<Patient> _dbSet;

    public PatientEFRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Patient>();
    }

    public async Task<Result<Patient>> AddAsync(Patient patient, CancellationToken cancellationToken)
    {
        var result = new Result<Patient>();

        if (patient is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Patient cannot be null.");
            return result;
        }

        try
        {
            _dbSet.Add(patient);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            result.IsSuccess = true;
            result.Data = patient;
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

    public async Task<Result<List<Patient>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = new Result<List<Patient>>();
        try
        {
            var list = await _dbSet.ToListAsync(cancellationToken).ConfigureAwait(false);
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
}
