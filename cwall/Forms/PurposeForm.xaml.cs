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
        public Purpose newPurpose = null;
        public Purpose editPurpose = null;
        public Random rand = new Random();
        //создание новой цели
        public PurposeForm(List<Purpose> purposes)
        {
            this.purposes = purposes;
            InitializeComponent();
        }


        public PurposeForm(List<Purpose> purposes, Purpose editPurpose)
        {
            this.purposes = purposes;
            this.editPurpose = editPurpose;
            InitializeComponent();
            cname.Text = editPurpose.Name;
            cdesk.Text = editPurpose.Description;
            cprice.Text = editPurpose.Price.ToString();
            int index = 0;
            string val = editPurpose.valute;
            val = val.Trim();
            if (val == "RUB")
            {
                index = 0;
            }

            if (val == "DOL")
            {
                index = 1;
            }


            if (val == "EURO")
            {
                index = 2;
            }


            if (val == "LAR")
            {
                index = 3;
            }

            cur.SelectedIndex = index;



        }



        private void createNew(object sender, RoutedEventArgs e)
        {
            bool val = validate();
            if (val == false)
            {
                return;
            }

            if (editPurpose!= null)
            {
                editPurpose.Name = cname.Text;
                editPurpose.Description = cdesk.Text;
                editPurpose.valute = (cur.SelectedItem as ComboBoxItem).Content.ToString();
                editPurpose.Price = Convert.ToDouble(cprice.Text);
                DialogResult = true;
                return;
            }
            newPurpose = new Purpose();
            newPurpose.Id = getUniqId();
            newPurpose.Name = cname.Text;
            newPurpose.Description = cdesk.Text;
            newPurpose.valute = (cur.SelectedItem as ComboBoxItem).Content.ToString();
            newPurpose.Price = Convert.ToDouble(cprice.Text);
            newPurpose.Date = DateTime.Now;
            DialogResult = true;
        }

        public int getUniqId()
        {
            int r = rand.Next(10000);
            var obj = purposes.FirstOrDefault(o => o.Id == r);
            if (obj!= null)
            {
                MessageBox.Show("Фига себе " + r);
                return getUniqId();
            }

            return r;
        }

        public bool validate()
        {
            string name = cname.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Не пустое имя");
                return false;
            }

            string desc = cdesk.Text;
            if (String.IsNullOrEmpty(desc))
            {
                MessageBox.Show("Не пустое описание");
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
