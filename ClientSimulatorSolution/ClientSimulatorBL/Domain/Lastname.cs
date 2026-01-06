using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class Lastname : Name
    {
        public Lastname(string nameName, int? frequency) : base(nameName, frequency)
        {
        }
    }
}
