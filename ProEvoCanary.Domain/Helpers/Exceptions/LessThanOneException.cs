using System;

namespace ProEvoCanary.Domain.Helpers.Exceptions
{
    public class LessThanOneException : Exception
    {
        private readonly string _message;
        public LessThanOneException() { }

        public LessThanOneException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get { return _message; }
        }
    }
}