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
    /// Логика взаимодействия для CapitalForm.xaml
    /// </summary>
    public partial class CapitalForm : MetroWindow
    {
        public CapitalForm()
        {
            InitializeComponent();
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
    }
}
