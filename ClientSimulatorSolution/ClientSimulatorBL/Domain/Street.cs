using ClientSimulatorBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class Street
    {
        public Street(string name, string municipality)
        {
            Name = name;
            Municipality = municipality;
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) _name = value;
                else throw new DataReadingException("Name empty");
            }
        }

        private string _municipality;

        public string Municipality
        {
            get
            {
                return _municipality;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value != "(unknown)") 
                {
                    string[] removeKommune = value.Split(' ');
                    List<string> toArrange = new List<string>();  
                    foreach (string s in removeKommune)
                    {
                        if (s != "Kommune") toArrange.Add(s);
                    }


                    _municipality = string.Join(" ", toArrange);

                }
                else throw new DataReadingException("Municipality empty or unknown");
            }
        }

    }
}
