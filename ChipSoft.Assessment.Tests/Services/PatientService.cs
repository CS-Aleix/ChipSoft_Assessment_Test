using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Services;
using ChipSoft.Assessment.Domain.Entities;
using ChipSoft.Assessment.Domain.Enums;
using NSubstitute;

namespace ChipSoft.Assessment.Tests.Services
{
    public class PatientServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddPatient_ShouldAddPatient_WhenAddingCorrectPatient()
        {
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(1990, 1, 1),
                Gender = Gender.Male
            };

            var repository = Substitute.For<IPatientRepository>();            

            var _sut = new PatientService(repository);
            //_sut.AddPatientAsync(patient);

            repository.Received(1).AddAsync(Arg.Any<Patient>(), Arg.Any<CancellationToken>());
        }
    }
}
