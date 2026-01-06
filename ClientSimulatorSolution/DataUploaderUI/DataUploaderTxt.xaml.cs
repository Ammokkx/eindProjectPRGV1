using ClientSimulatorBL.Enums;
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

namespace DataUploaderUI
{
    /// <summary>
    /// Interaction logic for DataUploaderTxt.xaml
    /// </summary>
    public partial class DataUploaderTxt : Window
    {
        private string _path;
        private string _country;
        private string _connectionstring;
        public DataUploaderTxt(string path, string country, string connectionstring)
        {
            InitializeComponent();
            _path = path;
            _country = country;
            _connectionstring = connectionstring;
        }

        private void ButtonUploadNames_Click(object sender, RoutedEventArgs e)
        {
            DataUploaderTxtNames n = new DataUploaderTxtNames(_path, _country, _connectionstring);
            n.ShowDialog();
        }

        private void ButtonUploadStreets_Click(object sender, RoutedEventArgs e)
        {
            DataUploaderTxtStreets s = new DataUploaderTxtStreets(_path, _country, _connectionstring);
            s.ShowDialog();
        }
    }
}
