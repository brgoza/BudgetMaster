using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetMaster.Common;

public class Error
{
    public ErrorType ErrorType { get; set; }
    public string? Message { get; set; }
    public Error(ErrorType errorType, string? message = null)
    {
        ErrorType = errorType;
        Message = message;
    }
}
public enum ErrorType
{
    ValidationError,
    None,
    NotFound,
    Invalid,
    Duplicate,
    Unauthorized,
    Forbidden,
    Server
}
