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


    }
}
