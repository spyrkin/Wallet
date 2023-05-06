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
        public List<Payment> payments;

        public Payment newPayment = null;

        public Random rand = new Random();

        public PaymentForm(Purpose purpose, List<Payment> payments)
        {
            this.purpose = purpose;
            this.payments = payments;
            InitializeComponent();
            UpdateUi();

        }

        public int getUniqId()
        {
            int r = rand.Next(10000);
            var obj = payments.FirstOrDefault(o => o.Id == r);
            if (obj != null)
            {
                MessageBox.Show("Фига себе " + r);
                return getUniqId();
            }

            return r;
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

            newPayment = new Payment();
            newPayment.Id = getUniqId();
            newPayment.purposeId = purpose.Id;
            newPayment.Name = cname.Text;
            newPayment.valute = purpose.valute;
            newPayment.Price = Convert.ToDouble(cprice.Text);
            newPayment.Date = DateTime.Now;
            DialogResult = true;
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

        private void addK(object sender, RoutedEventArgs e)
        {
            cname.Text = "кальян";
            cprice.Text = "30";
        }
    }
}
