using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
using ClientSimulatorBL.Interfaces;
using ClientSimulatorBL.Managers;
using ClientSimulatorBL.SimulationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for MakeSimulationWindow.xaml
    /// </summary>
 
    public partial class MakeSimulationWindow : Window
    {
        private SimulationService _simulationService;
        private ICountryRepository _countryRepository;
        private INameRepository _nameRepository;
        private IStreetRepository _streetRepository;
        internal ObservableCollection<string> municipalities;
        const int TestYear = 2022;
        const int defaultSeed = 1;
        public MakeSimulationWindow(SimulationService simulationService, ICountryRepository icountryRepository, INameRepository nameRepository, IStreetRepository streetRepository)
        {
            InitializeComponent();
            _simulationService = simulationService;
            _countryRepository = icountryRepository;
            _nameRepository = nameRepository;
            _streetRepository = streetRepository;
            TextBoxFrequencyLetters.Text = "0";
            TextBoxMaxAge.Text = "0";
            TextBoxMaxHouseNumber.Text = "0";
            TextBoxMinAge.Text = "0";
            TextBoxCustomers.Text = "100";
            municipalities = new ObservableCollection<string>();
            ComboBoxCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            ComboBoxCountry.SelectedIndex = 0;
            ListBoxMunicipalities.ItemsSource = municipalities;
         
        }

        private void ButtonMunicipalities_Click(object sender, RoutedEventArgs e)
        {
            AddMunicipalitiesWindow m = new AddMunicipalitiesWindow(_streetRepository, municipalities, _countryRepository.GetCountryIDByName(ComboBoxCountry.SelectedItem.ToString()), TestYear);
            m.ShowDialog();
        }

      

        private void ButtonMakeSimulation_Click(object sender, RoutedEventArgs e)
        {
            int countryId = _countryRepository.GetCountryIDByName(ComboBoxCountry.SelectedItem.ToString());
            PeopleSimulator p = new PeopleSimulator(_nameRepository.GetFirstNamesByCountryIDAndYear(countryId, TestYear),
                _nameRepository.GetLastNamesByCountryIDAndYear(countryId, TestYear),
                _streetRepository.GetAllStreetsByCountryIDAndYear(countryId, TestYear),
                int.Parse(TextBoxMinAge.Text),
                int.Parse(TextBoxMaxAge.Text),
                int.Parse(TextBoxMaxHouseNumber.Text),
                int.Parse(TextBoxFrequencyLetters.Text),
                1
                )
                ;
            List<SimulatedPerson> persons = p.GeneratePerson(int.Parse(TextBoxCustomers.Text));

        }
    }
}
