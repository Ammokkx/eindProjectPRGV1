using ClientSimulatorBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class SimulatedPerson
    {
        public SimulatedPerson(string firstname, string lastname, string gender, Street address, string houseNr, DateTime birthDate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            HouseNr = houseNr;
            BirthDate = birthDate;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public Street Address { get; set; }
        public string HouseNr { get; set; }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ClientSimException("Birthdate is in the future.");
                }
                else
                {
                    _birthDate = value;
                }
            }
        }
        public override string ToString() 
        {
            return $"{Firstname}, {Lastname}, {Gender}, {Address.Name}, {Address.Municipality}, {HouseNr}, {BirthDate.ToString()}";
        }
    }
}
