namespace TravelExperienceEgypt.API.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public string? Error { get; init; }

        public static Result<T> Success(T data) => new Result<T>() { IsSuccess = true, Data = data };
        public static Result<T> Failure(string msg) => new Result<T>() { IsSuccess = false, Error = msg };
    }
}
