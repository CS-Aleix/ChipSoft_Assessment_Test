namespace ChipSoft.Assessment.Application.Interfaces.Services;

public interface IAppDbContext
{
    Task<(bool error, string message)> ResetAndReseedAsync();
}
