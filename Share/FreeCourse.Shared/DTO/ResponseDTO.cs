using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.DTO
{
    public class ResponseDTO<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; private set; }

        public List<String> Errors { get; set; }

        public static ResponseDTO<T> Success(T data , int statusCode)
        {
            return new ResponseDTO<T>{ Data = data, StatusCode = statusCode, IsSuccessfull = true};
        }

        public static ResponseDTO<T> Success(int statusCode) 
        {
            return new ResponseDTO<T>{ Data = default(T), StatusCode = statusCode, IsSuccessfull= true};
        }

        public static ResponseDTO<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDTO<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }

        public static ResponseDTO<T> Fail(string error, int statusCode) 
        {
            return new ResponseDTO<T>
            {
                Errors = new List<string>() { error },
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
    }
}
