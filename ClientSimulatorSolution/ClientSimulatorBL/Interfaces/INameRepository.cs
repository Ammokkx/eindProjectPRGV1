using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Interfaces
{
    public interface INameRepository
    {
        List<Firstname> GetFirstNamesByCountryIDAndYear(int id, int year);
        List<Lastname> GetLastNamesByCountryIDAndYear(int id, int year);
        void UploadToDatabase(List<Name> data, string country, int year, NameType nameType);
    }
}
