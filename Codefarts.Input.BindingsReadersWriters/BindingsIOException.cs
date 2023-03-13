using System.Runtime.Serialization;

namespace Codefarts.Input;

public class BindingsIOException : Exception
{
    public BindingsIOException()
    {
    }

    protected BindingsIOException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BindingsIOException(string? message) : base(message)
    {
    }

    public BindingsIOException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}