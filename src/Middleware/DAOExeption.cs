using System;
using System.Runtime.Serialization;

namespace Meetup
{
    public class DAOExeption : Exception
    {
        public DAOExeption(DAOErrorType type)
        {
            Type = type;
        }

        public DAOExeption(DAOErrorType type,string? message) : base(message)
        {
            Type = type;
        }

        public DAOExeption(DAOErrorType type,string? message, Exception? innerException) : base(message, innerException)
        {
            Type = type;
        }
        public DAOErrorType Type { get; set; }
    }
}
