using ClientDBSimUtils;
using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
using ClientSimulatorBL.Interfaces;
using ClientSimulatorDL.DataBaseRepos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
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

namespace DataUploaderUI
{
    /// <summary>
    /// Interaction logic for DataUploaderTxtNames.xaml
    /// </summary>
    public partial class DataUploaderTxtNames : Window
    {
        private string _path;
        private string _country;
        private string _connectionstring;
        private ICountryRepository _countryRepository;
        private INameRepository _nameRepository;
        private RepositoryFactory _repositoryFactory;
        private IDataReaderCSV _dataReaderCSV;
        public DataUploaderTxtNames(string path, string country, string connectionstring)
        {
            InitializeComponent();
            _path = path;
            _country = country;
            _connectionstring = connectionstring;
            _repositoryFactory = new RepositoryFactory();
            _nameRepository = _repositoryFactory.GiveNameRepository(_connectionstring);
            _countryRepository = _repositoryFactory.GiveCountryRepository(_connectionstring);
            _dataReaderCSV = _repositoryFactory.GiveDataReaderCSV();
        }

        private void CheckboxFirstName_Click(object sender, RoutedEventArgs e)
        {
            if (CheckboxLastName.IsChecked == true) 
            {
                CheckboxLastName.IsChecked = false;
            }
        }

        private void CheckboxLastName_Click(object sender, RoutedEventArgs e)
        {
            if (CheckboxFirstName.IsChecked == true)
            {
                CheckboxFirstName.IsChecked = false;
            }
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadingStatusWindow uploading = new UploadingStatusWindow("Uploading data...", false);
            uploading.Show();

            List<Name> data = new List<Name>();
            string? gender = null;
            NameType nameType;
            if (CheckboxFemale.IsChecked == true)
                {
                gender = "Female";

            }
            else if (CheckboxMale.IsChecked == true)
            {
                gender = "Male";
            }
            if (CheckboxFirstName.IsChecked == false && CheckboxLastName.IsChecked == false)
            {
                uploading.Close();
                uploading = new UploadingStatusWindow("Please specify first or last name.", true);
                uploading.Show();
                return;
            }


            if (CheckboxFirstName.IsChecked == true)
            {
                nameType = NameType.First_Name;
                if (CheckboxFemale.IsChecked == false && CheckboxMale.IsChecked == false)
                {
                    uploading.Close();
                    uploading = new UploadingStatusWindow("Please specify gender.", true);
                    uploading.Show();
                    return;
                }

                if (TextBoxColumnFrequency.Text.IsNullOrEmpty())
                {
                  data = _dataReaderCSV.ReadFirstNamesWithoutFrequency(_path, TextBoxSeparator.Text.First(), int.Parse(TextBoxLinesToSkip.Text), int.Parse(TextBoxColumnName.Text), gender);
                    _nameRepository.UploadToDatabase(data, _country, int.Parse(TextBoxYear.Text), nameType);
                }
                else
                {
                  data = _dataReaderCSV.ReadFirstNamesWithFrequency(_path, TextBoxSeparator.Text.First(), int.Parse(TextBoxLinesToSkip.Text), int.Parse(TextBoxColumnName.Text), gender, int.Parse(TextBoxColumnFrequency.Text));
                    _nameRepository.UploadToDatabase(data, _country, int.Parse(TextBoxYear.Text), nameType);
                }

            }
            else if (CheckboxLastName.IsChecked == true)
            {
                nameType = NameType.Last_Name;
                if (TextBoxColumnFrequency.Text.IsNullOrEmpty())
                {
                    data = _dataReaderCSV.ReadLastNamesWithoutFrequency(_path, TextBoxSeparator.Text.First(), int.Parse(TextBoxLinesToSkip.Text), int.Parse(TextBoxColumnName.Text));
                    _nameRepository.UploadToDatabase(data, _country, int.Parse(TextBoxYear.Text), nameType);
                }
                else
                {
                    data = _dataReaderCSV.ReadLastNamesWithFrequency(_path, TextBoxSeparator.Text.First(), int.Parse(TextBoxLinesToSkip.Text), int.Parse(TextBoxColumnName.Text), int.Parse(TextBoxColumnFrequency.Text));
                    _nameRepository.UploadToDatabase(data, _country, int.Parse(TextBoxYear.Text), nameType);
                }
            }

            



                uploading.Close();
            uploading = new UploadingStatusWindow("Finished uploading.", true);
            uploading.Show();
        }

        private void CheckboxMale_Click(object sender, RoutedEventArgs e)
        {
            if (CheckboxFemale.IsChecked == true) 
            { 
                CheckboxFemale.IsChecked = false;
            }
        }

        private void CheckboxFemale_Click(object sender, RoutedEventArgs e)
        {
            if (CheckboxMale.IsChecked == true)
            {
                CheckboxMale.IsChecked = false;
            }
        }
    }
}
