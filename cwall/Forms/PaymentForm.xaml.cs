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

namespace cwall.Forms
{
    /// <summary>
    /// Логика взаимодействия для PaymentForm.xaml
    /// </summary>
    public partial class PaymentForm : MetroWindow
    {

        public Purpose purpose;


        public PaymentForm(Purpose purpose)
        {
            this.purpose = purpose;
            InitializeComponent();
            UpdateUi();

        }

        public void UpdateUi()
        {
            cpurpose.Content = purpose.Name;
        }



        //создание
        private void createNew(object sender, RoutedEventArgs e)
        {
            bool val = validate();
            if (val == false)
            {
                return;
            }
        }


        public bool validate()
        {
            string name = cname.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Не пустая причина");
                return false;
            }


            string vpice = cprice.Text;
            if (String.IsNullOrEmpty(vpice))
            {
                MessageBox.Show("Не пустой платеж");
                return false;

            }

            double number;
            if (!Double.TryParse(vpice, out number))
            {
                MessageBox.Show("Платеж должна быть валидной");
                return false;
            }

            if (number < 0)
            {
                MessageBox.Show("Платеж должен быть больше нуля");
                return false;
            }

            return true;
        }
    }
}
