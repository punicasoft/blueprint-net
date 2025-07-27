namespace Punica.Bp
{
    public class ResultOf<TResult> : ResultOf<TResult, ErrorResult>
    {
        //public static ResultOf<TResult> Success(TResult data)
        //{
        //    return new ResultOf<TResult>
        //    {
        //        IsSuccess = true,
        //        Data = data
        //    };
        //}

        //public static ResultOf<TResult> Failure(ErrorResult error)
        //{
        //    return new ResultOf<TResult>
        //    {
        //        IsSuccess = false,
        //        Error = error
        //    };
        //}

        ////Automatic type conversion for T to ResultOf<T>
        //public static implicit operator ResultOf<TResult>(TResult data)
        //{
        //    return Success(data);
        //}

        ////Automatic type conversion for ErrorResult to ResultOf<T>
        //public static implicit operator ResultOf<TResult>(ErrorResult error)
        //{
        //    return Failure(error);
        //}

    }

    public class ResultOf<TResult, TError>
    {
        public bool IsSuccess { get; set; }
        public TResult? Data { get; set; }
        public TError? Error { get; set; }

        public static ResultOf<TResult, TError> Success(TResult data)
        {
            return new ResultOf<TResult, TError>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static ResultOf<TResult, TError> Failure(TError error)
        {
            return new ResultOf<TResult, TError>
            {
                IsSuccess = false,
                Error = error
            };
        }

        //Automatic type conversion for TResult to ResultOf<T>
        public static implicit operator ResultOf<TResult, TError>(TResult data)
        {
            return Success(data);
        }

        //Automatic type conversion for TError to ResultOf<T>
        public static implicit operator ResultOf<TResult, TError>(TError error)
        {
            return Failure(error);
        }
    }

    public record ErrorResult
    {
        public int? Code { get; init; }
        public string? Message { get; init; }
        public object? Details { get; init; }


        public static ErrorResult BadRequest(string message, object? details = null)
        {
            return new ErrorResult
            {
                Code = 400,
                Message = message,
                Details = details
            };
        }

        public static ErrorResult InternalServerError(string message, object? details = null)
        {
            return new ErrorResult
            {
                Code = 500,
                Message = message,
                Details = details
            };
        }

        public static ErrorResult Unauthorized(string message, object? details = null)
        {
            return new ErrorResult
            {
                Code = 401,
                Message = message,
                Details = details
            };
        }

        public static ErrorResult Forbidden(string message, object? details = null)
        {
            return new ErrorResult
            {
                Code = 403,
                Message = message,
                Details = details
            };
        }

        public static ErrorResult NotFound(string message, object? details = null)
        {
            return new ErrorResult
            {
                Code = 404,
                Message = message,
                Details = details
            };
        }

        public static ErrorResult Conflict(string message)
        {
            return new ErrorResult
            {
                Code = 409,
                Message = message
            };
        }


    }
}
