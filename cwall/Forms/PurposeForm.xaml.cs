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
    /// Логика взаимодействия для PurposeForm.xaml
    /// </summary>
    public partial class PurposeForm : MetroWindow
    {
        public List<Purpose> purposes;

        //создание новой цели
        public PurposeForm(List<Purpose> purposes)
        {
            this.purposes = purposes;
            InitializeComponent();
        }

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
                MessageBox.Show("Не пустое имя");
                return false;
            }


            string vpice = cprice.Text;
            if (String.IsNullOrEmpty(vpice))
            {
                MessageBox.Show("Не пустая цена");
                return false;

            }

            double number;
            if (!Double.TryParse(vpice, out number))
            {
                MessageBox.Show("Цена должна быть валидной");
                return false;

            }


            var si = cur.SelectedItem;
            if (si == null)
            {
                MessageBox.Show("Выберите валюту");
                return false;

            }
            return true;
        }
    }
}
