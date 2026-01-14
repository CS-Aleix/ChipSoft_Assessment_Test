using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Services;

public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
{
    public async Task<Result<Invoice>> CreateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(invoice);

        // Basic validation
        if (invoice.Patient is null)
        {
            return new Result<Invoice>
            {
                IsSuccess = false,
                Errors = new List<string> { "Patient is required for an invoice." }
            };
        }

        if (invoice.Appointments == null || invoice.Appointments.Count == 0)
        {
            return new Result<Invoice>
            {
                IsSuccess = false,
                Errors = new List<string> { "At least one treatment is required for an invoice." }
            };
        }

        // Calculate totals (side-effect free)
        var total = invoice.CalculateTotalAmount();

        // Persist using repository
        var addResult = await invoiceRepository.AddAsync(invoice, cancellationToken).ConfigureAwait(false);
        if (!addResult.IsSuccess)
        {
            return new Result<Invoice>
            {
                IsSuccess = false,
                Errors = addResult.Errors
            };
        }

        return new Result<Invoice>
        {
            IsSuccess = true,
            Data = addResult.Data
        };
    }
}
