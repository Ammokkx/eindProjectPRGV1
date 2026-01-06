using ClientDBSimUtils;
using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Domain.DTO;
using ClientSimulatorBL.Interfaces;
using ClientSimulatorBL.SimulationService;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RepositoryFactory _repositoryFactory;
        private SimulationService _simulationService;
        private ICountryRepository _countryRepository;
        private IStreetRepository _streetRepository;
        private INameRepository _nameRepository;
        private string _connectionstring;
        private List<SimulationDTO> sims = new List<SimulationDTO>();
        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetSection("ConnectionStrings")["SQLserver"];
            _connectionstring = connectionString;


            _repositoryFactory = new RepositoryFactory();
            _countryRepository = _repositoryFactory.GiveCountryRepository(_connectionstring);
            _streetRepository = _repositoryFactory.GiveStreetRepository(_connectionstring);
            _nameRepository = _repositoryFactory.GiveNameRepository(_connectionstring);

            _simulationService = new SimulationService(_countryRepository, _nameRepository, _streetRepository, _repositoryFactory.GiveSimulationRepository(_connectionstring));

            sims = _simulationService.GetAllSimplifiedSimulations();
            DataGridSimulations.ItemsSource = sims;

        }

        private void DataGridSimulationsNew(object sender, RoutedEventArgs e)
        {
            MakeSimulationWindow m = new MakeSimulationWindow(_simulationService, _countryRepository, _nameRepository, _streetRepository);
            m.ShowDialog();

        }

        private void DataGridSimulationsDetails(object sender, RoutedEventArgs e)
        {
        if (DataGridSimulations.SelectedItem is SimulationDTO selection) 
            {
              SimulationDetailWindow s =  new SimulationDetailWindow(_simulationService.GetAllDetails(selection.ID));
                s.ShowDialog();
            }
        }
    }
}