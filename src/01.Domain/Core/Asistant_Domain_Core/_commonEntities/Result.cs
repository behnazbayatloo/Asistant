using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core._commonEntities
{
    public class Result<T>
    {
        
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Value { get; }
        public string Message { get; }
        protected Result(bool isSuccess, T value, string message) 
        { IsSuccess = isSuccess; Value = value; Message = message; }
        public static Result<T> Success(T value,string message) => new Result<T>(true,value, message);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);
    }
}
