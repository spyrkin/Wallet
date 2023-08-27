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

        Models.Purpose purpose;
        public PurposeListWindow(List<Models.Purpose> purposes)
        {
            purposes = purposes.OrderByDescending(o=>o.Date).ToList();
            this.purposes = purposes;
            InitializeComponent();
            dataGridView.ItemsSource = purposes;
            dataGridView.Items.Refresh();
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
