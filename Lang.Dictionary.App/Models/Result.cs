namespace Lang.Dictionary.App.Models
{
    public class Result<T>
    {
        public Result(T value)
        {
            IsSuccess = true;
            Value = value;
        }

        public Result()
        {
        }

        public bool IsSuccess { get; private set; }

        public string Error { get; private set; }

        public T Value { get; private set; }

        public static Result<T> CreateError(string error)
        {
            return new Result<T> { Error = error };
        }
    }
}
