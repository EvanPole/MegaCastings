using MegaCastings.View;
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

namespace MegaCastings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_CustomerClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new CustomerView();
        }

        private void Button_PartnerClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new PartnerView();
        }
        private void Button_OffersClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new OffersView();
        }
    }
}
