using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
using ClientSimulatorBL.Interfaces;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;

namespace ClientSimulatorDL.Readers
{
    public class DataReaderCSV : IDataReaderCSV
    {

        public List<Name> ReadFirstNamesWithoutFrequency(string path, char? separator, int linesToSkip, int positionName, string? gender)
        {
            List<Name> names = new();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                for (int i = 0; i > linesToSkip; i++)
                {
                    line = sr.ReadLine();
                }
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (separator != null)
                        {
                            names.Add(new Firstname(line.Split(separator.Value)[positionName - 1].Trim(), null, gender));

                        }
                        else
                        {
                            names.Add(new Firstname(line.Split('\t')[positionName - 1].Trim(), null, gender));

                        }
                    }
                    catch (Exception) { }
                }
            } 
            return names;
        }

        public List<Name> ReadFirstNamesWithFrequency(string path, char? separator, int linesToSkip, int positionName, string? gender, int positionFrequency)
        {
            List<Name> names = new();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                for (int i = 0; i > linesToSkip; i++)
                {
                    line = sr.ReadLine();
                }
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (separator != null)
                        {
                            names.Add(new Firstname(line.Split(separator.Value)[positionName - 1].Trim(), int.Parse(line.Split(separator.Value)[positionFrequency - 1].Trim()), gender));
                        }
                        else
                        {
                            names.Add(new Firstname(line.Split('\t')[positionName - 1].Trim(), int.Parse(line.Split('\t')[positionFrequency - 1].Trim()), gender));
                        }
                    }
                    catch (Exception) { }
                }
            }
            return names;
        }

        public List<Name> ReadLastNamesWithFrequency(string path, char? separator, int linesToSkip, int positionName, int positionFrequency)
        {
            List<Name> names = new();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                for (int i = 0; i > linesToSkip; i++)
                {
                    line = sr.ReadLine();
                }
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (separator != null)
                        {
                            names.Add(new Lastname(line.Split(separator.Value)[positionName - 1].Trim(), int.Parse(line.Split(separator.Value)[positionFrequency - 1].Trim())));
                        }
                        else
                        {
                            names.Add(new Lastname(line.Split('\t')[positionName - 1].Trim(), int.Parse(line.Split('\t')[positionFrequency - 1].Trim())));
                        }
                    }
                    catch (Exception) { }
                }
            }
            return names;
        }

        public List<Name> ReadLastNamesWithoutFrequency(string path, char? separator, int linesToSkip, int positionName)
        {
            List<Name> names = new();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                for (int i = 0; i > linesToSkip; i++)
                {
                    line = sr.ReadLine();
                }
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (separator != null)
                        {
                            names.Add(new Lastname(line.Split(separator.Value)[positionName - 1].Trim(), null));
                        }
                        else
                        {
                            names.Add(new Lastname(line.Split('\t')[positionName - 1].Trim(), null));
                        }
                    }
                    catch (Exception) { }
                }
            }
            return names;
        }

        public List<Street> ReadStreets(string path, char? separator, int linesToSkip, int positionName, int positionMunicipality, int positionHighwaytype)
        {
            List<Street> streets = new();
            using (StreamReader sr = new StreamReader(path))
            {
                string[] highwayTypes = Enum.GetNames(typeof(HighwayTypes));
                string line;
                for (int i = 0; i > linesToSkip; i++)
                {
                    line = sr.ReadLine();
                }
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (separator != null)
                        {
                            if (highwayTypes.Contains(line.Split(separator.Value)[positionHighwaytype - 1].Trim().ToLower()))
                            {
                                Street street = new Street(line.Split(separator.Value)[positionName - 1].Trim(), line.Split(separator.Value)[positionMunicipality - 1].Trim());

                               streets.Add(street);
                            }
                        }
                        else
                        {
                            if (highwayTypes.Contains(line.Split('\t')[positionHighwaytype - 1].Trim().ToLower()))
                            {
                                Street street = new Street(line.Split('\t')[positionName - 1].Trim(), line.Split('\t')[positionMunicipality - 1].Trim());
                               streets.Add(street);
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            return streets;
        }
    }
}
