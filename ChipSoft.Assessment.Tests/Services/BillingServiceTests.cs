using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Services;
using ChipSoft.Assessment.Domain.Entities;
using ChipSoft.Assessment.Domain.Enums;
using NSubstitute;

namespace ChipSoft.Assessment.Tests.Services;

public class BillingServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateInvoice_ShouldBillingPatient_WhenPatientIsProvided()
    {
        var repository = Substitute.For<IInvoiceRepository>();
        var patient = new Patient { FirstName = "Aleix", LastName = "Hernandez", DateOfBirth = new DateOnly(1988, 4, 15), Gender = Gender.Male };
        //var _sut = new BillingService(repository);

        //_sut.CreateInvoice(patient);
    }
}
