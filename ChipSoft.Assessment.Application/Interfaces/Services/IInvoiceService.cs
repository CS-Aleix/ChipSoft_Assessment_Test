using ChipSoft.Assessment.Domain.Entities;
using ChipSoft.Assessment.Domain.Classes;

namespace ChipSoft.Assessment.Application.Interfaces.Services;

public interface IInvoiceService
{
    Task<Result<Invoice>> CreateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default);
}
