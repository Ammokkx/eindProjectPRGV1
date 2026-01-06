using ClientSimulatorBL.Enums;
using ClientSimulatorBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public abstract class Name
    {
        protected Name(string nameName, int? frequency)
        {
            NameName = nameName;
            Frequency = frequency;
        }


        private string _nameName;
        public string NameName
        {
            get
            {
                return _nameName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) _nameName = value;
                else throw new DataReadingException("Name empty");
            }
        }

     
        
        private int? _frequency;

        public int? Frequency
        {
            get
            {
                return _frequency;
            }
            set
            {
                _frequency = value;
            }
        }
    }
}
