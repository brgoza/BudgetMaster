using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetMaster.Common;

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<Error>? Errors { get; set; }
    public Result(bool success, string? message = null, List<Error>? errors = null)
    {
        Success = success;
        Message = message;
        Errors = errors;
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }
    public Result(bool success, T? data = default, string? message = null, List<Error>? errors = null) : base(success, message, errors)
    {
        Data = data;
    }
}

