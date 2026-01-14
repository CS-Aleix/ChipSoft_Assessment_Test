namespace ChipSoft.Assessment.Domain.Classes;

public class Result<T> where T : class
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
}
