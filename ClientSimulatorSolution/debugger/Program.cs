using ClientSimulatorBL.Domain;
using ClientSimulatorDL.Readers;
using System.Data;

namespace debugger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataReaderCSV f = new DataReaderCSV();

            List<Street> ff = new List<Street>();

            ff = f.ReadStreets("C:\\Users\\Admin\\Downloads\\SourceData\\Denemarken\\denmark_streets2.csv", ';', 1, 2, 1, 3);

            

            
        }
    }
}
