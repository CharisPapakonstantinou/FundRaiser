using System.Net.Http;

namespace FundRaiser.Common.Dto
{
    
    
    public class ApiResult<T>
    {
        public T Data { get; }
        public string Message { get; }
        public bool Success { get; }
        
        public ApiResult(T data, bool success, string message) 
        {
            Data = data;
            Success = success;
            Message = message;
        }
        
        public ApiResult(T data) 
        {
            Data = data;
            Success = true;
            Message = "Completed";
        }
    }
}