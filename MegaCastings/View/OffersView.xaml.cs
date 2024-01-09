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
    /// Logique d'interaction pour OffersView.xaml
    /// </summary>
    public partial class OffersView : Page
    {
        public ObservableCollection<Announce> allOffer { get; set; }

        public Announce? SelectedOffer { get; set; }
        public OffersView()
        {

            InitializeComponent();

            this.DataContext = this;


            using (MegaProductionContext allOffers = new())
            {
                allOffer = new ObservableCollection<Announce>(allOffers.Announces.ToList());
            }

            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Title", "Titre" },
                { "Content", "Description" },
                { "Bigcategoryid", "Catégorie" },
                { "Subcategoryid", "Sous Catégorie" },
                { "Datetime", "Date" },
                { "Contracttypeid", "Type de contract" },
                { "Hourlyrate", "€/H" },
            };

            foreach (var column in columnsToDisplay)
            {
                DataGridTextColumn dataColumn = new DataGridTextColumn();
                dataColumn.Header = column.Value;
                dataColumn.Binding = new Binding(column.Key);
                offersdatagrid.Columns.Add(dataColumn);
            }

            offersdatagrid.AutoGenerateColumns = false;

        }

        private void Button_RemovePartner(object sender, RoutedEventArgs e)
        {
            if (this.allOffer != null && SelectedOffer != null)
            {
                using (MegaProductionContext context = new())
                {
                    context.Announces.Remove(SelectedOffer);
                    context.SaveChanges();
                    this.allOffer.Remove(SelectedOffer);
                }
            }
        }
    }
}
