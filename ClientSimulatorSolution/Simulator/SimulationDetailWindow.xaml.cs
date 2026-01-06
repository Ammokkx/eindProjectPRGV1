using ClientSimulatorBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Simulator
{
    /// <summary>
    /// Interaction logic for SimulationDetailWindow.xaml
    /// </summary>
    public partial class SimulationDetailWindow : Window
    {
        private Simulation sim {  get; set; }
        public SimulationDetailWindow(Simulation sim)
        {
            InitializeComponent();
            this.sim = sim;
            ListBoxMaleNames.ItemsSource = PopulateFirstNamesTable("Male");
            ListBoxFemaleNames.ItemsSource = PopulateFirstNamesTable("Female");
            ListBoxFamilyNames.ItemsSource = PopulateLastNamesTable();
            ListBoxStreets.ItemsSource = PopulateStreetDetails();
            ListBoxGeneralDetails.ItemsSource = PopulateGeneralDetails();
        }

        private List<string> PopulateGeneralDetails()
        {
            List<string> items = new();
            items.Add($"Seed used: {sim.Seed}");
            items.Add($"Country selected: {sim.Country}");
            items.Add($"Year selected: {sim.Year.ToString()}");
            items.Add($"Amount of cuystomers generated: {sim.SimulatedPeople.Count().ToString()}");
            items.Add($"Client: {sim.Client}");
            items.Add($"Date created: {sim.DateSimulated:dd-MM-yyyy}");
            SimulatedPerson p = sim.SimulatedPeople.OrderByDescending(x => x.BirthDate).First();
            items.Add($"Youngest customer: {p.Firstname} {p.Lastname}, {(DateTime.Now.Year) - p.BirthDate.Year} years old.");
            p = sim.SimulatedPeople.OrderBy(x => x.BirthDate).First();
            items.Add($"Oldest customer: {p.Firstname} {p.Lastname}, {(DateTime.Now.Year) - p.BirthDate.Year} years old.");
            items.Add($"Average age when simulated: {Math.Round(sim.DateSimulated.Year - sim.SimulatedPeople.Select(x => x.BirthDate.Year).Average())}");
            items.Add($"Average age now: {Math.Round(DateTime.Now.Year - sim.SimulatedPeople.Select(x => x.BirthDate.Year).Average())}");

            return items;
        }

        private List<string> PopulateStreetDetails()
        {
            Dictionary<string, List<string>> yes = new();
            List<string> names = new();

            foreach (SimulatedPerson p in sim.SimulatedPeople)
            {
                if (yes.ContainsKey(p.Address.Municipality))
                {
                    yes[p.Address.Municipality].Add(p.Address.Name);
                }
                else
                {
                    yes.Add(p.Address.Municipality, new List<string>());
                    yes[p.Address.Municipality].Add(p.Address.Name);
                }
            }

            var query = yes.Select(x => new { Name = x.Key, Count = x.Value.Count() });

            foreach (var result in query)
            {
                names.Add($"{result.Name}: {result.Count}");
            }
            return names;
        }
        private List<string> PopulateFirstNamesTable(string gender)
        {
            List<string> names = new List<string>();

            var query = sim.SimulatedPeople.Where(x => x.Gender == gender).Select(x => x.Firstname)
                .GroupBy(s => s)
                .Select(g => new { Name = g.Key, Count = g.Count() });

            foreach (var result in query)
            {
                names.Add($"{result.Name}: {result.Count}");
            }
            return names;
        }
        private List<string> PopulateLastNamesTable()
        {
            List<string> names = new List<string>();

            var query = sim.SimulatedPeople.Select(x => x.Lastname)
                .GroupBy(s => s)
                .Select(g => new { Name = g.Key, Count = g.Count() });

            foreach (var result in query)
            {
                names.Add($"{result.Name}: {result.Count}");
            }
            return names;
        }

    }
}
