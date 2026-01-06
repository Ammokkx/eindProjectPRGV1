using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorBL.Interfaces
{
    public interface IDataReaderCSV
    {
        List<Name> ReadFirstNamesWithFrequency(string path, char? separator, int linesToSkip, int positionName, string? gender, int positionFrequency);
        List<Name> ReadFirstNamesWithoutFrequency(string path, char? separator, int linesToSkip, int positionName, string? gender);
        List<Name> ReadLastNamesWithFrequency(string path, char? separator, int linesToSkip, int positionName, int positionFrequency);
        List<Name> ReadLastNamesWithoutFrequency(string path, char? separator, int linesToSkip, int positionName);
        List<Street> ReadStreets(string path, char? separator, int linesToSkip, int positionName, int positionMunicipality, int positionHighwaytype);
    }
}
