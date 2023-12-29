using MegaCastings.View;
using MegaCastings;
using System;
using System.Collections.ObjectModel; // Ajout de la directive pour ObservableCollection
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MegaCastings.DBLib.Class;

namespace MegaCastings.View
{
    /// <summary>
    /// Logique d'interaction pour CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {
        public ObservableCollection<User> alluser { get; set; } // Utilisation de ObservableCollection au lieu de List

        public User? SelectedUser { get; set; }

        public CustomerView()
        {
            InitializeComponent();

            this.DataContext = this;

            using (MegaProductionContext allusers = new())
            {
                alluser = new ObservableCollection<User>(allusers.Users.ToList());
            }
        }
        private void Button_EditCustomer(object sender, RoutedEventArgs e)
        {
            Main.Content = new EditCustomerView(SelectedUser);
        }
        private void Button_AddCustomer(object sender, RoutedEventArgs e)
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

        private void Button_RemoveCustomer(object sender, RoutedEventArgs e)
        {
            if (this.alluser != null && SelectedUser != null)
            {
                using (MegaProductionContext context = new())
                {
                    context.Users.Remove(SelectedUser);
                    context.SaveChanges();
                    this.alluser.Remove(SelectedUser);
                }
            }
        }
    }
}
