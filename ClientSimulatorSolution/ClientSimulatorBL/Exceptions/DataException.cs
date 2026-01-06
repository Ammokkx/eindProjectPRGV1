using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Exceptions
{
    internal class DataException : Exception
    {
        public DataException()
        {
        }

        public DataException(string? message) : base(message)
        {
        }

        public DataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    
    }
    
}
