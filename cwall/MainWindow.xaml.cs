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

            double c = s * 100 / current.Price;
            string proc = (c.ToString("#.##")).ToString() + " %";

            lost.Content = current.Price - s;
            lpoc.Content = proc;

        }

        public void setCurrentPurlose()
        {
            purposes = purposes.OrderBy(o => o.Date).ToList();
            if (purposes.Count() > 0)
            {
                current = purposes.First();
            }

            curPayments = payments.Where(o => o.purposeId == current.Id).ToList();

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
    }
}
