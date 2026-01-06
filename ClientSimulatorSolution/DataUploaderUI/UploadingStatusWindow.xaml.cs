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
    /// Interaction logic for UploadingStatusWindow.xaml
    /// </summary>
    public partial class UploadingStatusWindow : Window
    {
        private string _content;
        private bool _buttonVisibility;
        public UploadingStatusWindow(string content, bool buttonVisibility)
        {
            InitializeComponent();
            _content = content;
            _buttonVisibility = buttonVisibility;
            TextBoxStatus.Text = _content;
            if (_buttonVisibility)
            {
                ButtonConfirm.Visibility = Visibility.Visible;
            }

        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
