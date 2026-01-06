using ClientSimulatorBL.Enums;
using ClientSimulatorBL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
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

namespace DataUploaderUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private string _fileSelected;
        private string _connectionstring;
        private readonly ICountryRepository _countryRepository;
        private readonly IDataReaderCSV _dataReaderCSV;
        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetSection("ConnectionStrings")["SQLserver"];
            _connectionstring = connectionString;

            fileDialog.Filter = "Choose (.txt;.csv;.json)|*.txt;*.csv;*.json";
            fileDialog.Multiselect = false;
            ComboBoxCountry.ItemsSource = Enum.GetValues(typeof(Countries));
        }

        private void ButtonChooseFile_Click(object sender, RoutedEventArgs e)
        {
            bool? result = fileDialog.ShowDialog();
            if (result == true && !string.IsNullOrWhiteSpace(fileDialog.FileName))
            {
                _fileSelected = fileDialog.FileName;
                TextBoxFilePath.Text = _fileSelected;
            }

        }

        private void ButtonUploader_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxFilePath.Text != string.Empty && TextBoxFilePath.Text != null)
            {
                    DataUploaderTxt d = new DataUploaderTxt(TextBoxFilePath.Text, ComboBoxCountry.SelectedItem.ToString(), _connectionstring);
                    d.ShowDialog();
            }
        }
    }
}