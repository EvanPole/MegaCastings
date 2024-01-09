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
    /// Représente la logique de l'interface utilisateur pour l'affichage des offres d'emploi.
    /// </summary>
    public partial class OffersView : Page
    {
        // Collection pour contenir toutes les offres d'emploi
        public ObservableCollection<Announce> allOffer { get; set; }

        // Offre d'emploi actuellement sélectionnée
        public Announce? SelectedOffer { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="OffersView"/>.
        /// </summary>
        public OffersView()
        {
            // Initialise les composants de l'interface utilisateur
            InitializeComponent();

            // Définit le contexte de données pour la liaison de données
            this.DataContext = this;

            // Récupère toutes les offres d'emploi depuis la base de données et remplit la collection
            using (MegaProductionContext allOffers = new())
            {
                allOffer = new ObservableCollection<Announce>(allOffers.Announces.ToList());
            }

            // Définit les colonnes à afficher dans le DataGrid
            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Title", "Titre" },
                { "Content", "Description" },
                { "Bigcategoryid", "Catégorie" },
                { "Subcategoryid", "Sous Catégorie" },
                { "Datetime", "Date" },
                { "Contracttypeid", "Type de contrat" },
                { "Hourlyrate", "€/H" },
            };

            // Génère dynamiquement les colonnes du DataGrid en fonction du dictionnaire défini
            foreach (var column in columnsToDisplay)
            {
                DataGridTextColumn dataColumn = new DataGridTextColumn();
                dataColumn.Header = column.Value;
                dataColumn.Binding = new Binding(column.Key);
                offersdatagrid.Columns.Add(dataColumn);
            }

            // Désactive la génération automatique des colonnes pour utiliser celles prédéfinies
            offersdatagrid.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Gestionnaire d'événements pour le bouton de suppression d'une offre d'emploi sélectionnée.
        /// </summary>
        private void Button_RemovePartner(object sender, RoutedEventArgs e)
        {
            // Vérifie s'il y a une offre d'emploi sélectionnée et si la collection n'est pas nulle
            if (this.allOffer != null && SelectedOffer != null)
            {
                // Supprime l'offre d'emploi sélectionnée de la base de données et met à jour la collection
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
