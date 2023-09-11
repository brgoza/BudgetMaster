using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetMaster.Common;

public class Result
{
    public bool IsSuccess => _success;
    public bool IsFailure => !_success;
    private readonly bool _success;
    public string? Message { get; set; }
    public List<Error>? Errors { get; set; }
    protected Result(bool success, string? message = null, List<Error>? errors = null)
    {
        _success = success;
        Message = message;
        Errors = errors;
    }
    public static Result Failure(string? message = "failure")
        => new(false, message);

    public static Result Success(string? message = "success")
        => new(true, message);
}

public class Result<T> : Result
{
    public T? Data { get; set; }
    public Result(bool success, T? data = default, string? message = null, List<Error>? errors = null) : base(success, message, errors)
    {
        Data = data;
    }
    public static Result<T> Failure(string? message, T? data = default)
        => new(false, data, message);
    public static Result<T> Success(T? data, string? message = null)
        => new(true, data, message);
    
        
}

