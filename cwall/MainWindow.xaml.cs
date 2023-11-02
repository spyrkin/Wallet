﻿using cwall.Forms;
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
            DateTime gr = new DateTime(2023, 1, 11);
            var timespan = DateTime.Now - gr;
            var days = Math.Floor(timespan.TotalDays);
            Console.WriteLine("!!!!!!!!!!");
            Console.WriteLine(days);
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
            List<double> proc = new List<double>();
            getRecursicProc(proc);
            foreach (var item in proc)
            {
                Console.WriteLine(item);
            }
            purposes = Purpose.LoadFromFile();
            payments = Payment.LoadFromFile();
            purposes = purposes.OrderBy(o => o.Date).ToList();
            payments = payments.OrderBy(o => o.Date).ToList();
            foreach (var item in purposes)
            {
                item.work(payments);
            }



            setCurrentPurlose();
            draw();
        }

        private void getRecursicProc(List<double> proc)
        {

            if (proc.Count == 5) return;
            double _max = 100;
            double _closest = 0;
            if (proc.Count > 0)
            {
                _closest = proc.Last();
            }
            double _new = (_max + _closest) / 2;
            proc.Add(_new);
            getRecursicProc(proc);
        }

        public void draw()
        {
            lpurs.Content = current.Name;
            ldesc.Text = current.Description;
            lprice.Content = current.Price;

            double s = 0;
            foreach (var p in curPayments)
            {
                s = s + p.Price;
            }

            lvalute.Content = getSimbol();
            if (s > current.Price)
            {
                s = current.Price;
            }
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
                current = purposes.FirstOrDefault(o=>o.isCurrentPurpose);
                if (current == null)
                {
                    current = purposes.Last();
                }
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

            if (curPayments.Count == 0)
            {
                return;
            }
            //foreach (var p in curPayments)
            //{
            //    s = s + p.Price;
            //}
            //if (s > current.Price)
            //{
            //    s = current.Price;
            //}

            //var startPrice = curPayments.Last();
            //DateTime now = DateTime.Now;
            //if (s == current.Price)
            //{
            //    now = curPayments[0].Date;
            //}
            //var span = now - startPrice.Date;
            //double span_dayts = span.TotalDays;

            //span_dayts = Math.Floor(span_dayts) + 1;
            //double ave = s / span_dayts;
            result = "Collect: "+current.collect;
            result = result +"\nAverage: " + current.ave;
            //double proc_days = Math.Ceiling(current.Price / ave);
            //DateTime purposeday = startPrice.Date.AddDays(proc_days);
            result = result + "\nThink: : " + current.think;

            //var span2 = purposeday - now;
            //int ost = (int)span2.TotalDays;
            //if (ost < 0)
            //{
            //    ost = 0;
            //}
            result = result + "\nOst Days: : " + current.ost;
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

        private void onShowPurposes(object sender, MouseButtonEventArgs e)
        {
            var form = new PurposeListWindow(purposes, payments);
            form.ShowDialog();

            if (form.DialogResult == true)
            {
                Purpose.SaveToFile(purposes);
                reload();
            }
        }

        private void openCapitalForm(object sender, RoutedEventArgs e)
        {
            var form = new CapitalForm();
            form.ShowDialog();
        }
    }
}
