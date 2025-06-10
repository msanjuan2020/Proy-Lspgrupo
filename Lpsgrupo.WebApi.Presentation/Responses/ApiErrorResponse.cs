namespace Lpsgrupo.WebApi.Presentation.Responses
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(string type, string traceId)
        {
            Type = type;
            //ErrorCode = errorCode;
            TraceId = traceId;
            Message = string.Empty;
        }

        public ApiErrorResponse(string type, string message, string traceId)
        {
            Type = type;
            Message = message;
            //ErrorCode = errorCode;
            TraceId = traceId;
        }

        public string Type { get; set; }
        public string Message { get; set; }
        //public int ErrorCode { get; set; }
        public string TraceId { get; set; }
    }
}
