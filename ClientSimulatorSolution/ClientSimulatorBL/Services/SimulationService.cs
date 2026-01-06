using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Domain.DTO;
using ClientSimulatorBL.Interfaces;
using ClientSimulatorBL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.SimulationService
{
    public class SimulationService
    {
        private ICountryRepository _countryrepo;
        private INameRepository _namerepo;
        private IStreetRepository _streetrepo;
        private ISimulationRepository _simulationrepo;
        public SimulationService(ICountryRepository countryrepo, INameRepository namerepo, IStreetRepository streetrepo, ISimulationRepository simulationrepo)
        {
            _countryrepo = countryrepo;
            _namerepo = namerepo;
            _streetrepo = streetrepo;
            _simulationrepo = simulationrepo;
        }
      public  void UploadToDatabase(List<SimulatedPerson> data, string country, int year, string client, DateTime date, int seed)
        {
           _simulationrepo.UploadToDatabase(data, country, year, client, date, seed);
        }
        public List<SimulationDTO> GetAllSimplifiedSimulations()
        {
            return _simulationrepo.GetAllSimplifiedSimulations();
        }
        public Simulation GetAllDetails(int id)
        {
            return _simulationrepo.GetAllSimDetails(id);
        }

    }
}
