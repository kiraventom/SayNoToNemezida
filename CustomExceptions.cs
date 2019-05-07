using System;

namespace CustomExceptions
{
    [Serializable]
    public class IncorrectSeparatorException : Exception
    {
        public IncorrectSeparatorException(string message) : base(message)
        {

        }
    }
}
