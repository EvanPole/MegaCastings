using MegaCastings.DBLib.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MegaCastings.View
{
    /// <summary>
    /// Représente la logique de l'interface utilisateur pour l'ajout d'un partenaire.
    /// </summary>
    public partial class AddPartnerView : Page
    {
        // Collections pour contenir les catégories principales et les packs
        public ObservableCollection<BigCategory> BigCategories { get; set; }
        public ObservableCollection<Pack> Packs { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="AddPartnerView"/>.
        /// </summary>
        public AddPartnerView()
        {
            // Initialise les composants de l'interface utilisateur
            InitializeComponent();

            // Définit le contexte de données pour la liaison de données
            DataContext = this;

            // Récupère les catégories principales et les packs depuis la base de données
            BigCategories = GetBigCategories();
            Packs = GetPackCategories();
        }

        /// <summary>
        /// Obtient les catégories principales depuis la base de données.
        /// </summary>
        private ObservableCollection<BigCategory> GetBigCategories()
        {
            // Obtient les catégories depuis la base de données
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<BigCategory>(context.BigCategories.ToList());
            }
        }

        /// <summary>
        /// Obtient les packs depuis la base de données.
        /// </summary>
        private ObservableCollection<Pack> GetPackCategories()
        {
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<Pack>(context.Packs.ToList());
            }
        }

        /// <summary>
        /// Événement déclenché lors du clic sur le bouton d'ajout d'un partenaire.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Récupère les valeurs saisies par l'utilisateur
            string Label = label.Text;
            string Siret = siret.Text;
            string Desc = desc.Text;
            DateTime? selectedDate = date.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;

            // Vérifie si les champs obligatoires sont remplis
            if (!string.IsNullOrEmpty(Label) && !string.IsNullOrEmpty(Siret) && !string.IsNullOrEmpty(Desc) && selectedDate.HasValue)
            {
                // Vérifie la sélection de la catégorie principale et du pack
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory &&
                    DropdownPack.SelectedItem is Pack selectedPack)
                {
                    // Crée un nouveau partenaire avec les données saisies
                    Partner newPartner = new Partner
                    {
                        Label = Label,
                        Siret = Siret,
                        Desc = Desc,
                        Datetime = selectedDate,
                        Isactive = CheckBoxIsActive,
                        Bigcategoryid = selectedBigCategory.Id,
                        Packid = selectedPack.Id,
                    };

                    // Ajoute le nouveau partenaire à la base de données
                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Partners.Add(newPartner);
                        context.SaveChanges();
                    }

                    // Affiche un message de succès
                    MessageBox.Show("Partenaire ajouté avec succès.");

                    // Redirige vers la vue des partenaires
                    PartnerView PartnerView = new PartnerView();
                    NavigationService?.RemoveBackEntry(); // Efface l'historique de navigation
                    NavigationService?.Navigate(PartnerView);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.");
            }
        }
    }
}
