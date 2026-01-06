using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class Firstname : Name
    {
        public Firstname(string nameName, int? frequency, string? Gender) : base(nameName, frequency)
        {
            this.Gender = Gender;
        }

        public string? Gender { get; set; }
    }
}
