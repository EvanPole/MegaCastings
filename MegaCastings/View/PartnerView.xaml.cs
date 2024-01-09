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
        public ObservableCollection<Partner> allpartner { get; set; } // Utilisation de ObservableCollection au lieu de List

        public Partner? SelectedPartner { get; set; }
        public PartnerView()
        {
            
            InitializeComponent();

            this.DataContext = this;

            using (MegaProductionContext allpartners = new())
            {
                allpartner = new ObservableCollection<Partner>(allpartners.Partners.ToList());
            }

            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "Label", "Label" },
                { "Siret", "Siret" },
                { "Desc", "Description" },
                { "Datetime", "Date de création" },
                { "Bigcategoryid", "Catégorie" },
                { "Packid", "Pack" },
                { "Isactive", "Actif" }
            };

            foreach (var column in columnsToDisplay)
            {
                DataGridTextColumn dataColumn = new DataGridTextColumn();
                dataColumn.Header = column.Value;
                dataColumn.Binding = new Binding(column.Key);
                partnersdatagrid.Columns.Add(dataColumn);
            }

            // Supprimez les colonnes du XAML
            partnersdatagrid.AutoGenerateColumns = false;
        }

        private void Button_EditPartner(object sender, RoutedEventArgs e)
        {
            Main.Content = new EditPartnerView(SelectedPartner);
        }
        private void Button_AddPartner(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddPartnerView();
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
                    context.Partners.Remove(SelectedPartner);
                    context.SaveChanges();
                    this.allpartner.Remove(SelectedPartner);
                }
            }
        }
    }
}