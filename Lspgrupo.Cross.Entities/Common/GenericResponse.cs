using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lspgrupo.Cross.Entities.Common;

public class GenericResponse<T>
{
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static GenericResponse<T> Success(T data, string message = "") =>
        new GenericResponse<T> { IsSuccess = true, Message = message, Data = data };

    public static GenericResponse<T> Fail(string message = "") =>
        new GenericResponse<T> { IsSuccess = false, Message = message, Data = default };
}
