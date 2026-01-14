using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Services;

public class BillingService(IInvoiceService invoiceService, IAppointmentService appointmentService) : IBillingService
{
    public void CreateInvoice(Patient patient)
    {
        if (patient is null) throw new ArgumentNullException(nameof(patient));
    }
}
