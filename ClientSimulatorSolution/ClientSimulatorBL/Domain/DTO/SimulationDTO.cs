using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain.DTO
{
    public class SimulationDTO
    {
        public SimulationDTO(int id, string client, string country)
        {
            ID = id;
            Client = client;
            Country = country;
        }

        public int ID {  get; set; }

        public string Client {  get; set; }
        public string Country { get; set; }
    }
}
