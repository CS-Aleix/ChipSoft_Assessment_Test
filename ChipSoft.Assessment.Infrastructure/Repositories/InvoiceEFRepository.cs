using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class InvoiceEFRepository : GenericEFRepository<Invoice>, IInvoiceRepository
{
    public InvoiceEFRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
