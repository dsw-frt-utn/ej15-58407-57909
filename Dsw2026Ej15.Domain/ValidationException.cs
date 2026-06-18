using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
