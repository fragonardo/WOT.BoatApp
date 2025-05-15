using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Domain.Exceptions;

public class BoatException : Exception
{
    public BoatException(string message) : base(message)
    { }        
}

public class BoatConflictException : Exception
{
    public BoatConflictException(string message) : base(message)
    { }
}

public class BoatNotFoundException : Exception
{
    public BoatNotFoundException(string message) : base(message)
    { }
}
