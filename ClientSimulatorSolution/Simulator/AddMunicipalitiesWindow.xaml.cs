using ClientSimulatorBL.Interfaces;
using ClientSimulatorDL.DataBaseRepos;
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
    /// Interaction logic for AddMunicipalitiesWindow.xaml
    /// </summary>
    public partial class AddMunicipalitiesWindow : Window
    {
        private IStreetRepository _streetrepo {  get; set; }
        private ObservableCollection<string> _allmunicipalities;
        private ObservableCollection<string> _currentmunicipalities;

        public AddMunicipalitiesWindow(IStreetRepository street, ObservableCollection<string> currentMuni, int id, int year)
        {
            InitializeComponent();
            _streetrepo = street;
            _currentmunicipalities = currentMuni;
            _allmunicipalities = new ObservableCollection<string>(_streetrepo.GetAllMunicipalitiesByCountryIDAndYear(id, year));
            ListBoxAllMunicipalities.ItemsSource = _allmunicipalities;
            ListBoxSelectedMunicipalities.ItemsSource = _currentmunicipalities;
        }


        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (string s in _currentmunicipalities) _allmunicipalities.Add(s);
            _currentmunicipalities.Clear();
        }

        private void ButtonRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new();
            foreach (string s in ListBoxSelectedMunicipalities.SelectedItems) data.Add(s);
            foreach (string s in data)
            {
                _allmunicipalities.Add(s);
                _currentmunicipalities.Remove(s);
            }
        }

        private void ButtonAddSelected_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new();
            foreach (string s in ListBoxAllMunicipalities.SelectedItems) data.Add(s);
            foreach (string s in data)
            {
                _currentmunicipalities.Add(s);
                _allmunicipalities.Remove(s);
            }
        }

        private void ButtonAddAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (string s in _allmunicipalities) _currentmunicipalities.Add(s);
            _allmunicipalities.Clear();
        }
    }
}
