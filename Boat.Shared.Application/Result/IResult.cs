namespace BoatApp.Shared.Application;

public interface IResult
{
    ResultStatus Status { get; }
    IEnumerable<string> Errors { get; }
    bool IsSuccess { get; }    
}
