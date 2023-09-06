using cwall.Models;
using MahApps.Metro.Behaviors;
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

        public List<Capital> capitals = new List<Capital>();
        public Random rand = new Random();

        public CapitalForm()
        {
            InitializeComponent();
            reload();


        }

        public void reload()
        {
            cprice.Text = "";
            capitals = Capital.LoadFromFile();
            capitals = capitals.OrderBy(o => o.Date).ToList();
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

        public int getUniqId()
        {
            int r = rand.Next(10000);
            var obj = capitals.FirstOrDefault(o => o.Id == r);
            if (obj != null)
            {
                MessageBox.Show("Фига себе " + r);
                return getUniqId();
            }

            return r;
        }

        private void onAddCapital(object sender, RoutedEventArgs e)
        {
            bool val = validate();
            if (val == false)
            {
                return;
            }
            DateTime date = DateTime.Now;
            var capital = new Capital();
            capital.Id = getUniqId();
            capital.lary = Convert.ToDouble(cprice.Text);
            capital.Date = date;

            //get prev month
            var today = date;
            var month = new DateTime(today.Year, today.Month, 1);
            var lastmonthday = month.AddMonths(-1);
            string Name = lastmonthday.ToString("MMMM-yyyy");
            capital.Name = Name;
            capitals.Add(capital);
            Capital.SaveToFile(capitals);
            reload();
        }
    }
}
