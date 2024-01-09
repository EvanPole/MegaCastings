using MegaCastings.DBLib.Class;
using Org.BouncyCastle.Bcpg;
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
    /// Logique d'interaction pour EditPartnerView.xaml
    /// </summary>
    public partial class EditPartnerView : Page
    {
        private Partner _Partner;
        public ObservableCollection<BigCategory>? BigCategories { get; set; }

        public Partner Partner
        {
            get { return _Partner; }
            set { _Partner = value; }
        }

        public EditPartnerView(Partner partner)
        {
            InitializeComponent();


            this.Partner = partner;
            if (partner == null)
            {
                PartnerView PartnerView = new PartnerView();
                NavigationService?.RemoveBackEntry();
                NavigationService?.Navigate(PartnerView);
            }
            else
            {
                InitializeComponent();
                DataContext = this;
                this.label.Text = partner.Label;
                this.siret.Text = partner.Siret;
                this.desc.Text = partner.Desc;
                this.date.SelectedDate = partner.Datetime;
                this.DropdownBigCategories.ItemsSource = GetBigCategories();
                this.DropdownPack.ItemsSource = GetPackCategories();
                this.checkboxisactive.IsChecked = partner.Isactive == 1 ? true : false;
            }
        }

        private ObservableCollection<BigCategory> GetBigCategories()
        {
            // Obtenez les catégories depuis la base de données
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<BigCategory>(context.BigCategories.ToList());
            }
        }
        private ObservableCollection<Pack> GetPackCategories()
        {
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<Pack>(context.Packs.ToList());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Label = label.Text;
            string Siret = siret.Text;
            string Desc = desc.Text;
            DateTime? selectedDate = date.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;

            if (!string.IsNullOrEmpty(Label) && !string.IsNullOrEmpty(Siret) && !string.IsNullOrEmpty(Desc) && selectedDate.HasValue)
            {

                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory && DropdownPack.SelectedItem is Pack selectedPack)
                {
                    Partner.Label = Label;
                    Partner.Siret = Siret;
                    Partner.Desc = Desc;
                    Partner.Datetime = selectedDate;
                    Partner.Isactive = CheckBoxIsActive;
                    Partner.Bigcategoryid = selectedBigCategory.Id;
                    Partner.Packid = selectedPack.Id;

                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Partners.Update(Partner);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Utilisateur update avec succès.");

                    PartnerView PartnerView = new PartnerView();
                    NavigationService?.RemoveBackEntry(); // Efface l'historique de navigation
                    NavigationService?.Navigate(PartnerView);


                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une catégorie et une sous-catégorie.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom et le nom de l'utilisateur.");
            }
        }
    }
}
