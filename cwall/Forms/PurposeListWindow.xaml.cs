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
    /// Логика взаимодействия для PurposeListWindow.xaml
    /// </summary>
    public partial class PurposeListWindow : MetroWindow
    {
        List<Models.Purpose> purposes;
        List<Models.Payment> payments;


        Models.Purpose purpose;
        public PurposeListWindow(List<Models.Purpose> purposes, List<Models.Payment> payments)
        {
            purposes = purposes.OrderByDescending(o=>o.Date).ToList();
            this.purposes = purposes;
            this.payments = payments;
            InitializeComponent();
            dataGridView.ItemsSource = purposes;
            dataGridView.Items.Refresh();
            calc();

            
        }

        public void calc()
        {
            double summ = 0;
            foreach (var item in payments)
            {
                summ = summ + item.Price;
            }
            ltotal.Content = "Total: " + summ;

            Payment first = payments[0];
            var span = DateTime.Now - first.Date;
            double span_dayts = span.TotalDays;
            double ave = summ/span_dayts;
            lave.Content = "Ave: " + ave.ToString("#.##");
        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Escape")
            {
                Close();
            }
        }

        private void onDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listpurposes = dataGridView.SelectedItems.Cast<Models.Purpose>().ToList();
            if (listpurposes.Count != 1 ) {
                return;
            }
            purpose = listpurposes[0];
            foreach (var item in purposes)
            {
                item.isCurrentPurpose = false; ;
            }
            purpose.isCurrentPurpose = true;
            DialogResult = true;
        }
    }
}
