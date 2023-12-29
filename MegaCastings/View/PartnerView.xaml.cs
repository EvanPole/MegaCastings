using MegaCastings.DBLib.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MegaCastings.View
{
    /// <summary>
    /// Logique d'interaction pour PartnerView.xaml
    /// </summary>
    public partial class PartnerView : Page
    {
        public ObservableCollection<User> allpartner { get; set; } // Utilisation de ObservableCollection au lieu de List

        public User? SelectedPartner { get; set; }
        public PartnerView()
        {
            InitializeComponent();

            this.DataContext = this;

            using (MegaProductionContext allpartners = new())
            {
                allpartner = new ObservableCollection<User>(allpartners.Users.ToList());
            }
        }

        private void Button_EditPartner(object sender, RoutedEventArgs e)
        {
            Main.Content = new EditCustomerView(SelectedPartner);
        }
        private void Button_AddPartner(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddCustomerView();
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

        private void Button_RemovePartner(object sender, RoutedEventArgs e)
        {
            if (this.allpartner != null && SelectedPartner != null)
            {
                using (MegaProductionContext context = new())
                {
                    context.Users.Remove(SelectedPartner);
                    context.SaveChanges();
                    this.allpartner.Remove(SelectedPartner);
                }
            }
        }
    }
}
