using System;

namespace ProEvoCanary.Domain.Helpers.Exceptions
{
    public class TooManyPlayersReturnedException : Exception
    {
        private readonly string _message;
        public TooManyPlayersReturnedException() { }
        public TooManyPlayersReturnedException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get { return _message; }
        }
    }
}