using cwall.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cwall
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public List<Payment> payments;
        public List<Purpose> purposes;
        public List<Payment> curPayments;
        public Purpose current;

        public DateTime date;
        public MainWindow()
        {
            InitializeComponent();
            reload();
        }

        private void addNewPurpose(object sender, RoutedEventArgs e)
        {
            var form = new PurposeForm(purposes);
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                purposes.Add(form.newPurpose);
                Purpose.SaveToFile(purposes);
                reload();
            }
        }

    


        public void reload()
        {
            purposes = Purpose.LoadFromFile();
            payments = Payment.LoadFromFile();
            setCurrentPurlose();
            draw();
        }

        public void draw()
        {
            lpurs.Text = current.Name;
            ldesc.Text = current.Description;
            lprice.Content = current.Price;

            double s = 0;
            foreach (var p in curPayments)
            {
                s = s + p.Price;
            }

            lvalute.Content = getSimbol();
            double c = s * 100 / current.Price;
            string proc = (c.ToString("#.##")).ToString() + " %";
            pg.Value = c;
            lost.Content = current.Price - s;
            lpoc.Content = proc;

            dataGridView.ItemsSource = curPayments;
            dataGridView.Items.Refresh();
        }

        public void setCurrentPurlose()
        {
            purposes = purposes.OrderBy(o => o.Date).ToList();
            if (purposes.Count() > 0)
            {
                current = purposes.First();
            }

            curPayments = payments.Where(o => o.purposeId == current.Id).OrderByDescending(o=>o.Date).ToList();

        }


        //заплатить
        private void addPayment(object sender, RoutedEventArgs e)
        {
            var form = new PaymentForm(current, payments);
            form.ShowDialog();

            if (form.DialogResult == true)
            {
                payments.Add(form.newPayment);
                Payment.SaveToFile(payments);
                reload();
            }
        }

        private void showList(object sender, RoutedEventArgs e)
        {

        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private string getSimbol()
        {
            if (current!= null && current.valute == "LAR")
            {
                return "₾";
            }
            return "";
        }

        private void onToolOpen(object sender, ToolTipEventArgs e)
        {
            Button b = sender as Button;
            string result = "";
            double s = 0;
            foreach (var p in curPayments)
            {
                s = s + p.Price;
            }
            var startPrice = curPayments.Last();
            DateTime now = DateTime.Now;
            var span = now - startPrice.Date;
            double span_dayts = span.TotalDays;
            double ave = s / span_dayts;
            result = "Collect: "+s.ToString();
            result = result +"\nAverage: " + ave.ToString("#.##");
            double proc_days = current.Price / ave;
            DateTime purposeday = startPrice.Date.AddDays(proc_days);
            result = result + "\nThink: : " + purposeday.ToString("yyyy-MM-dd");

            var span2 = purposeday - now;
            result = result + "\nOst Days: : " + (int)span2.TotalDays;
            b.ToolTip = result;
        }

        private void editPurpose(object sender, RoutedEventArgs e)
        {
            var form = new PurposeForm(purposes, current);
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                Purpose.SaveToFile(purposes);
                reload();
            }
        }
    }
}
