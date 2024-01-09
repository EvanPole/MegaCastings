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
using System.Collections.Generic;

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

            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "Lastname", "Nom" },
                { "Firstname", "Prénom" },
                { "Email", "E-mail" },
                { "Birthdate", "Date de naissance" },
                { "Bigcategoryid", "Catégorie" },
                { "Subcategoryid", "Sous-catégorie" },
                //{ "Annonceid", "Annonce ID" },
                { "Isactive", "Actif" }
            };

            foreach (var column in columnsToDisplay)
            {
                DataGridTextColumn dataColumn = new DataGridTextColumn();
                dataColumn.Header = column.Value;
                dataColumn.Binding = new Binding(column.Key);
                customersdatagrid.Columns.Add(dataColumn);
            }

            // Supprimez les colonnes du XAML
            customersdatagrid.AutoGenerateColumns = false;
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
