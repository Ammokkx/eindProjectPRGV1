using ClientSimulatorBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Interfaces
{
    public interface IStreetRepository
    {
        List<string> GetAllMunicipalitiesByCountryIDAndYear(int id, int year);
        List<Street> GetAllStreetsByCountryIDAndYear(int id, int year);
        void UploadToDatabase(List<Street> data, string country, int year);
    }
}
