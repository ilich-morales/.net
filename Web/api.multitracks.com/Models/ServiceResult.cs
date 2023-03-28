namespace api.multitracks.com.Models
{
    public class ServiceResult
    {
        public bool Succeeded => Error != null;
        public ErrorResult Error { get; set; }

        public ServiceResult()
        {
        }

        public ServiceResult(ErrorResult error)
        {
            Error = error;
        }

        public static ServiceResult Failed(ErrorResult error)
        {
            return new ServiceResult(error);
        }
        public static ServiceResult<T> Failed<T>(ErrorResult error)
        {
            return new ServiceResult<T>(error);
        }
        public static ServiceResult<T> Failed<T>(T data, ErrorResult error)
        {
            return new ServiceResult<T>(data, error);
        }
        public static ServiceResult<T> Success<T>(T data)
        {
            return new ServiceResult<T>(data);
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
        public ServiceResult(T data)// : base()
        {
            Data = data;
        }

        public ServiceResult(T data, ErrorResult error) : base(error)
        {
            Data = data;
        }

        public ServiceResult(ErrorResult error) : base(error)
        {

        }
    }
}
