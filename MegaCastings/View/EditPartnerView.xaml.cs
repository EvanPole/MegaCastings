using MegaCastings.DBLib.Class;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MegaCastings.View
{
    /// <summary>
    /// Représente la logique de l'interface utilisateur pour l'édition d'un partenaire.
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

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EditPartnerView"/>.
        /// </summary>
        public EditPartnerView(Partner partner)
        {
            InitializeComponent();

            // Initialise le partenaire à éditer
            this.Partner = partner;

            // Vérifie si le partenaire est null (nouvelle édition) et redirige vers la vue des partenaires
            if (partner == null)
            {
                PartnerView PartnerView = new PartnerView();
                NavigationService?.RemoveBackEntry();
                NavigationService?.Navigate(PartnerView);
            }
            else
            {
                // Remplit les champs de l'interface utilisateur avec les données du partenaire existant
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

        /// <summary>
        /// Obtient les catégories principales depuis la base de données.
        /// </summary>
        private ObservableCollection<BigCategory> GetBigCategories()
        {
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<BigCategory>(context.BigCategories.ToList());
            }
        }

        /// <summary>
        /// Obtient les catégories de packs depuis la base de données.
        /// </summary>
        private ObservableCollection<Pack> GetPackCategories()
        {
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<Pack>(context.Packs.ToList());
            }
        }

        /// <summary>
        /// Événement déclenché lors du clic sur le bouton de sauvegarde des modifications du partenaire.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Label = label.Text;
            string Siret = siret.Text;
            string Desc = desc.Text;
            DateTime? selectedDate = date.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;

            // Vérifie si les champs obligatoires sont remplis
            if (!string.IsNullOrEmpty(Label) && !string.IsNullOrEmpty(Siret) && !string.IsNullOrEmpty(Desc) && selectedDate.HasValue)
            {
                // Vérifie la sélection des catégories principale et de pack
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory && DropdownPack.SelectedItem is Pack selectedPack)
                {
                    // Met à jour les propriétés du partenaire avec les nouvelles données
                    Partner.Label = Label;
                    Partner.Siret = Siret;
                    Partner.Desc = Desc;
                    Partner.Datetime = selectedDate;
                    Partner.Isactive = CheckBoxIsActive;
                    Partner.Bigcategoryid = selectedBigCategory.Id;
                    Partner.Packid = selectedPack.Id;

                    // Met à jour le partenaire dans la base de données
                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Partners.Update(Partner);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Partenaire mis à jour avec succès.");

                    // Redirige vers la vue des partenaires
                    PartnerView PartnerView = new PartnerView();
                    NavigationService?.RemoveBackEntry(); // Efface l'historique de navigation
                    NavigationService?.Navigate(PartnerView);
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une catégorie principale et une catégorie de pack.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.");
            }
        }
    }
}
