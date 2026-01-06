using ClientSimulatorBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClientSimulatorBL.Managers
{
    public class PeopleSimulator
    {
        private Random _random;
        private List<Firstname> _firstnames = new List<Firstname>();
        private List<Lastname> _lastnames = new List<Lastname>();
        private List<Street> _streets = new List<Street>();
        private int _minAge, _maxAge, _maxHouseNr, _percentLetters;
        private int? _totalfrequencyFirstNames, _totalfrequencyLastNames;

        public PeopleSimulator(List<Firstname> firstnames, List<Lastname> lastnames, List<Street> streets, int minAge, int maxAge, int maxHouseNr, int percentLetters, int seed)
        {
            _firstnames = firstnames;
            _lastnames = lastnames;
            _streets = streets;
            _minAge = minAge;
            _maxAge = maxAge;
            _maxHouseNr = maxHouseNr;
            _percentLetters = percentLetters;
            _random = new Random(seed);
            _totalfrequencyFirstNames = firstnames.Sum(x => x.Frequency);
            _totalfrequencyLastNames = lastnames.Sum(x => x.Frequency);
        }

        public List<SimulatedPerson> GeneratePerson(int amount)
        {
            List<SimulatedPerson> data = new List<SimulatedPerson>();
            int amountMade = 0;
            string firstname = "";
            string lastname = "";
            string gender = "";
            while (amountMade < amount)
            {
                if(_totalfrequencyFirstNames != null)
                {
                    Firstname name = GetNameFromFrequency(GenerateFrequencyNumber(_totalfrequencyFirstNames.Value), _firstnames);
                    firstname = name.NameName;
                    gender = GetGender(name);
                }
                else
                {
                    Firstname name = _firstnames[_random.Next(_firstnames.Count)];
                    firstname = name.NameName;
                    gender = GetGender(name);
                }
                if (_totalfrequencyLastNames != null)
                {
                    lastname = GetNameFromFrequency(GenerateFrequencyNumber(_totalfrequencyLastNames.Value), _lastnames).NameName;
                }
                else
                {
                    lastname  = _lastnames[_random.Next(_lastnames.Count)].NameName;
                }

                Street street = _streets[_random.Next(_streets.Count)];
                string houseNr = GetHouseNumber(_maxHouseNr, _percentLetters);
                DateTime birthDate = MakeBirthDate(_minAge, _maxAge);

                SimulatedPerson person = new SimulatedPerson(firstname, lastname, gender, street, houseNr, birthDate);

                if (!data.Contains(person))
                {
                    amountMade++;
                    data.Add(person);
                }


            }
            return data;


        }

        private Firstname GetNameFromFrequency(int frequency, List<Firstname> names)
        {
            foreach (var name in names)
            {
                frequency -= name.Frequency.Value;
                if (frequency >= 0)
                {
                    return name;
                }
            }
            return null;
        }
        private Lastname GetNameFromFrequency(int frequency, List<Lastname> names)
        {
            foreach (var name in names)
            {
                frequency -= name.Frequency.Value;
                if (frequency >= 0)
                {
                    return name;
                }
            }
            return null;
        }
        private int GenerateFrequencyNumber(int frequency)
        {
            return _random.Next(frequency + 1);
        }

        private DateTime MakeBirthDate(int minLeeftijd, int maxLeeftijd)
        {
            DateTime now = DateTime.Now;
            DateTime min = now.AddYears(-minLeeftijd);
            DateTime max = now.AddYears(-maxLeeftijd);
            TimeSpan span = max - min;
            double range = span.TotalSeconds;
            return max.AddSeconds(_random.NextDouble() * range);
        }
        private string GetGender(Firstname name)
        {
            if(name.Gender == null)
            {
                if (_random.Next(2) == 1)
                {
                    return "Male";
                }
                else
                {
                    return "Female";
                }
            }
            else
            {
                return name.Gender;
            }
        }
        private string GetHouseNumber(int maxAmount, int percentLetters)
        {
            char[] letters = ['B', 'C', 'D'];


            if (_random.Next(100) < percentLetters)
            {
                return _random.Next(1, maxAmount + 1).ToString() + letters[_random.Next(letters.Count())];
            }
            else
            {
                return _random.Next(1, maxAmount + 1).ToString();
            }

        }
    }
}
