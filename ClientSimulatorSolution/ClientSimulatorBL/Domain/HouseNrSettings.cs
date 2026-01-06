using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class HouseNrSettings
    {
        public HouseNrSettings(int maxHouseNr, int percentLetters)
        {
            this.maxHouseNr = maxHouseNr;
            this.percentLetters = percentLetters;
        }

        public int maxHouseNr {  get; set; }
        public int percentLetters { get; set; }


    }
}
