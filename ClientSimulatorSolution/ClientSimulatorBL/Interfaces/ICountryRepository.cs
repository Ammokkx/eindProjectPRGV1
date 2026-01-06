using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Interfaces
{
    public interface ICountryRepository
    {
        int GetCountryIDByName(string country);
        int GetCountryYearVersionIDByYear(string country, int year);
        bool IsCountryInData(string country);
        bool IsCountryYearInData(string country, int year);
        void UploadCountry(string country);
        void UploadCountryYearVersion(string country, int year);
    }
}
