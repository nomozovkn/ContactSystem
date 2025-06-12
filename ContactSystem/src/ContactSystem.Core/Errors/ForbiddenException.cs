using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Core.Errors;

public class ForbiddenException : BaseException
{
    public ForbiddenException() { }
    public ForbiddenException(String message) : base(message) { }
    public ForbiddenException(String message, Exception inner) : base(message, inner) { }
    protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
