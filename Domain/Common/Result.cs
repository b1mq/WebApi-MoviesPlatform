using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public  class Result
    {
        public bool isSuccess {  get; set; }
        public bool isFailure { get; set; }
        public string Error { get; set; }
        protected Result(bool isSuccess, string Error)
        {
            if(isSuccess && !string.IsNullOrEmpty(Error))
            {
                throw new InvalidOperationException("Succesfull result cannot have error message");
            }else if (!isSuccess && string.IsNullOrEmpty(Error))
            {
                throw new InvalidOperationException("Failure result must have an message..");
            }
            this.isSuccess = isSuccess;
            this.Error = Error;

        }
        public static Result Success() => new Result(true,string.Empty);
        public static Result Failure(string error) => new Result(false,error);
    }
    public class Result<T>:Result
    {
        private readonly T? _value;
        public T Value => isSuccess ? _value! : throw new InvalidOperationException("The value of a failure result can not be accessed");
        protected internal Result(T? value,bool isSuccess,string error):base(isSuccess, error) 
        {
            _value = value;
        }
        public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty);

        public static new Result<T> Failure(string error) => new Result<T>(default, false, error);
    }
}
