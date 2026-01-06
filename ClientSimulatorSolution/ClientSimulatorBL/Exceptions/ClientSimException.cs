using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Exceptions
{
    public class ClientSimException : Exception
    {
        public ClientSimException() 
        {
        }

        public ClientSimException(string? message) : base(message)
        {
        }

        public ClientSimException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
