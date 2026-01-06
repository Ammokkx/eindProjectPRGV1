using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Exceptions
{
    public class DataReadingException : Exception
    {
        public DataReadingException()
        {
        }

        public DataReadingException(string? message) : base(message)
        {
        }

        public DataReadingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    
    }
    
}
