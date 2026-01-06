using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Domain
{
    public class Simulation
    {
        public Simulation(int id, DateTime dateSimulated, int seed, string client, string country, int year, List<SimulatedPerson> simulatedPeople)
        {
            Id = id;
            DateSimulated = dateSimulated;
            Seed = seed;
            Client = client;
            Country = country;
            Year = year;
            SimulatedPeople = simulatedPeople;
        }

        public int Id { get; set; }
        public DateTime DateSimulated { get; set; }

        public int Seed { get; set; }

        public string Client {  get; set; }

        public string Country { get; set; }
        public int Year { get; set; }
        public List<SimulatedPerson> SimulatedPeople { get; set; }


    }
}
