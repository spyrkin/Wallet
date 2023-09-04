using cwall.Models;
using MahApps.Metro.Controls;
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
using System.Xml.Linq;

namespace cwall.Forms
{
    /// <summary>
    /// Логика взаимодействия для CapitalForm.xaml
    /// </summary>
    public partial class CapitalForm : MetroWindow
    {
        public CapitalForm()
        {
            InitializeComponent();
        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void onDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Escape")
            {
                Close();
            }
        }

        public bool validate()
        {


            string vpice = cprice.Text;
            if (String.IsNullOrEmpty(vpice))
            {
                MessageBox.Show("Не пустой");
                return false;

            }

            double number;
            if (!Double.TryParse(vpice, out number))
            {
                MessageBox.Show("Платеж должна быть валидной");
                return false;
            }


            return true;
        }

        private void onAddCapital(object sender, RoutedEventArgs e)
        {
            bool val = validate();
            if (val == false)
            {
                return;
            }

            var capital = new Capital();
            capital.Id = 100;
            capital.lary = Convert.ToDouble(cprice.Text);
            capital.Date = DateTime.Now;
            //capital.Name = capital.Date.Month.ToString();   
        }
    }
}
