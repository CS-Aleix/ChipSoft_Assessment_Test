using ChipSoft.Assessment.Domain.Enums;
using ChipSoft.Assessment.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChipSoft.Assessment.Domain.Entities;

public class Invoice : BaseEntity, IInvoice
{
    public required Patient Patient { get; set; }
    public required DateTime InvoiceDate { get; set; }
    public BillingMethods BillingMethod { get; set; } = BillingMethods.DirectPayment;
    public double TotalAmount { get; set; }

    [NotMapped]
    public List<IAppointment> Appointments { get; set; } = new List<IAppointment>();

    public double CalculateTotalAmount()
    {
        double total = 0.0;
        foreach (var appointment in Appointments)
        {
            foreach (var treatment in appointment.Treatments)
            {
                total += treatment.CalculateCost();
            }
        }
        return total;
    }
}