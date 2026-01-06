using ClientSimulatorBL.Interfaces;
using ClientSimulatorDL.DataBaseRepos;
using ClientSimulatorDL.Readers;

namespace ClientDBSimUtils
{
    public class RepositoryFactory
    {
        public ICountryRepository GiveCountryRepository(string connectionstring)
        {
            return new CountryRepository(connectionstring);
        }

        public IDataReaderCSV GiveDataReaderCSV() 
        {
            return new DataReaderCSV();
        }

        public INameRepository GiveNameRepository(string connectionstring) 
        {
            return new NameRepository(connectionstring);
        }

        public IStreetRepository GiveStreetRepository(string connectionstring) 
        {
            return new StreetRepository(connectionstring);
        }

        public ISimulationRepository GiveSimulationRepository(string connectionstring) 
        {
            return new SimulationRepository(connectionstring);
        }

    }
}
