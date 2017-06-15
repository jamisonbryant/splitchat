using System;
using SplitChat.Core.Model;

namespace SplitChat.Core.Error
{
    /// <summary>
    /// Thrown when a message is given a type that is not one of the valid types.
    /// </summary>
    public class InvalidMessageTypeException : ApplicationException
    {
        public InvalidMessageTypeException(string message) : base(message) { }
    }
}
