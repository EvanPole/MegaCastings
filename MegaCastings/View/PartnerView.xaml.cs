using MegaCastings.DBLib.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MegaCastings.View
{
    /// <summary>
    /// Représente la logique de l'interface utilisateur pour l'affichage des partenaires.
    /// </summary>
    public partial class PartnerView : Page
    {
        // Collection pour contenir tous les partenaires
        public ObservableCollection<Partner> allpartner { get; set; }

        // Partenaire actuellement sélectionné
        public Partner? SelectedPartner { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="PartnerView"/>.
        /// </summary>
        public PartnerView()
        {
            // Initialise les composants de l'interface utilisateur
            InitializeComponent();

            // Définit le contexte de données pour la liaison de données
            this.DataContext = this;

            // Récupère tous les partenaires depuis la base de données et remplit la collection
            using (MegaProductionContext allpartners = new())
            {
                allpartner = new ObservableCollection<Partner>(allpartners.Partners.ToList());
            }

            // Définit les colonnes à afficher dans le DataGrid
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

            // Génère dynamiquement les colonnes du DataGrid en fonction du dictionnaire défini
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

        // Événement pour le bouton d'édition d'un partenaire sélectionné
        private void Button_EditPartner(object sender, RoutedEventArgs e)
        {
            // Navigue vers la vue d'édition du partenaire avec le partenaire sélectionné
            Main.Content = new EditPartnerView(SelectedPartner);
        }

        // Événement pour le bouton d'ajout d'un nouveau partenaire
        private void Button_AddPartner(object sender, RoutedEventArgs e)
        {
            // Navigue vers la vue d'ajout de partenaire
            Main.Content = new AddPartnerView();
        }

        // Événement pour le bouton de navigation vers la vue des clients
        private void Button_CustomerClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new CustomerView();
        }

        // Événement pour le bouton de navigation vers la vue des partenaires
        private void Button_PartnerClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new PartnerView();
        }

        // Événement pour le bouton de navigation vers la vue des offres d'emploi
        private void Button_OffersClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new OffersView();
        }

        // Événement pour le bouton de suppression d'un partenaire sélectionné
        private void Button_RemovePartner(object sender, RoutedEventArgs e)
        {
            // Vérifie s'il y a un partenaire sélectionné et si la collection n'est pas nulle
            if (this.allpartner != null && SelectedPartner != null)
            {
                // Supprime le partenaire sélectionné de la base de données et met à jour la collection
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
