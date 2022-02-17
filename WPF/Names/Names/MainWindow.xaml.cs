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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Names
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int MaxEntries { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
            {
                lstNames.Items.Add(txtName.Text);
                txtName.Clear();
            }
            if (lstNames.Items.Count > 6)
            {
                lstNames.Items.Clear();
            }
        }

        private void ButtonDelName_Click(object sender, RoutedEventArgs e)
        {
            lstNames.Items.Clear();
        }

        private void DelAll_UpClick(object sender, MouseButtonEventArgs e)
        {
            lstNames.Items.Clear();
        }

        private void ButtonRandomize_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            lstNames.Items.Clear();
            
            List<string> names = ListONames.names;
            
            for (int i = 0; i < MaxEntries; i++)
            {
                bool assigned = false;
                while (!assigned)
                {
                    string name = names[random.Next(names.Count)];
                    if (!lstNames.Items.Contains(name))
                    {
                        lstNames.Items.Add(name);
                        assigned = true;
                    }
                }
            }
            
        }

        private void chkNumberInput(object sender, TextCompositionEventArgs e)
        {
            Regex r = new Regex("[^0-9]+");
            e.Handled = r.IsMatch(e.Text);
        }

        private void ButtonOpenOptions_Click(object sender, RoutedEventArgs e)
        {
            Options opt = new();
            opt.Show();
        }
    }
}
