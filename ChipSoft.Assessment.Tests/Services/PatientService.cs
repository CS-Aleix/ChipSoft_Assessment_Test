using ChipSoft.Assessment.Application.DTOs;
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
        public async Task AddPatient_ShouldAddPatient_WhenAddingCorrectPatient()
        {
            var patient = new PatientCreationDTO
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(1990, 1, 1),
                Gender = Gender.Male,
                InsuranceNumber = "INS123456"
            };

            var patientRepository = Substitute.For<IPatientRepository>();

            var _sut = new PatientService(patientRepository);
            await _sut.AddPatientAsync(patient);

            await patientRepository.Received(1).AddAsync(Arg.Any<Patient>(), Arg.Any<CancellationToken>());
        }
    }
}
