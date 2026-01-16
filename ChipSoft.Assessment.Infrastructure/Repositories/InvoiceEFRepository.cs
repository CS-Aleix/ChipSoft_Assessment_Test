using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

//public class InvoiceEFRepository : GenericEFRepository<Invoice>, IInvoiceRepository
public class InvoiceEFRepository : IInvoiceRepository
{
    public InvoiceEFRepository(AppDbContext dbContext)
    {
    }

    public Task<Result<Invoice>> AddAsync(Invoice invoice, CancellationToken cancellationToken) => throw new NotImplementedException();
}
