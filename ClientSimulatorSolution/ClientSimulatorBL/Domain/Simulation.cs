using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class Simulation
    {
        public Simulation(DateTime dateSimulated, int seed, string client)
        {
            DateSimulated = dateSimulated;
            Seed = seed;
            Client = client;
        }

        public Simulation(int id, DateTime dateSimulated, string client)
        {
            Id = id;
            DateSimulated = dateSimulated;
            Client = client;
        }

        public int Id { get; set; }
        public DateTime DateSimulated { get; set; }

        public int Seed { get; set; }

        public string Client {  get; set; }


    }
}
