using ClientSimulatorBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Interfaces
{
    public interface ISimulationRepository
    {
        void UploadToDatabase(List<SimulatedPerson> data, string country, int year, string client, DateTime date, int seed);
    }
}
