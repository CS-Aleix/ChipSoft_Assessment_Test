using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Repositories;

public interface IInvoiceRepository
{
    Task<Result<Invoice>> AddAsync(Invoice invoice, CancellationToken cancellationToken);
}
