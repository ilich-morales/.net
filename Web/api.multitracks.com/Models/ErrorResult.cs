namespace api.multitracks.com.Models
{
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }        
        public ErrorResult(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public static ErrorResult BadRequest = new ErrorResult(400, "The request data is not valid");
        public static ErrorResult Unauthorized = new ErrorResult(401, "The requested resource is protected and you're unauthorized");
        public static ErrorResult NotFound = new ErrorResult(404, "The requested resource is not found");
        public static ErrorResult InternalServerError = new ErrorResult(500, "An exception has ocurred internally on the server");
    }
}
