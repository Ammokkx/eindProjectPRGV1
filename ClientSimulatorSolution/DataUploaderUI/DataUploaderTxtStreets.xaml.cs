using ClientDBSimUtils;
using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
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
using System.Xml;

namespace DataUploaderUI
{
    /// <summary>
    /// Interaction logic for DataUploaderTxtStreets.xaml
    /// </summary>
    public partial class DataUploaderTxtStreets : Window
    {
        private string _path;
        private string _country;
        private string _connectionstring;
        private ICountryRepository _countryRepository;
        private IStreetRepository _streetRepisitory;
        private RepositoryFactory _repositoryFactory;
        private IDataReaderCSV _dataReaderCSV;
        public DataUploaderTxtStreets(string path, string country, string connectionstring)
        {
            InitializeComponent();
            _path = path;
            _country = country;
            _connectionstring = connectionstring;
            _repositoryFactory = new RepositoryFactory();
            _streetRepisitory = _repositoryFactory.GiveStreetRepository(_connectionstring);
            _countryRepository = _repositoryFactory.GiveCountryRepository(_connectionstring);
            _dataReaderCSV = _repositoryFactory.GiveDataReaderCSV();
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadingStatusWindow uploading = new UploadingStatusWindow("Uploading data...", false);
            uploading.Show();

            List<Street> data = _dataReaderCSV.ReadStreets(_path, TextBoxSeparator.Text.First(), int.Parse(TextBoxLinesToSkip.Text), int.Parse(TextBoxStreetColumn.Text), int.Parse(TextBoxMunicipalityColumn.Text), int.Parse(TextBoxColumnHighwayType.Text));

            if (!_countryRepository.IsCountryInData(_country))
            {
                _countryRepository.UploadCountry(_country);
            }

            _streetRepisitory.UploadToDatabase(data, _country, int.Parse(TextBoxYearData.Text));
            uploading.Close();
            uploading = new UploadingStatusWindow("Finished uploading.", true);
            uploading.Show();

        }
    }
}
