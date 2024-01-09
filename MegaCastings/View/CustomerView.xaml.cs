using MegaCastings.View;
using MegaCastings;
using System;
using System.Collections.ObjectModel; 
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MegaCastings.DBLib.Class;
using System.Collections.Generic;

namespace MegaCastings.View
{
    /// <summary>
    /// Représente la logique de l'interface utilisateur pour l'affichage des clients.
    /// </summary>
    public partial class CustomerView : Page
    {
        // Collection pour contenir tous les utilisateurs (clients)
        public ObservableCollection<User> alluser { get; set; } // Utilisation de ObservableCollection au lieu de List

        // Utilisateur actuellement sélectionné
        public User? SelectedUser { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CustomerView"/>.
        /// </summary>
        public CustomerView()
        {
            // Initialise les composants de l'interface utilisateur
            InitializeComponent();

            // Définit le contexte de données pour la liaison de données
            this.DataContext = this;

            // Récupère tous les utilisateurs depuis la base de données et remplit la collection
            using (MegaProductionContext allusers = new())
            {
                alluser = new ObservableCollection<User>(allusers.Users.ToList());
            }

            // Définit les colonnes à afficher dans le DataGrid
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

            // Génère dynamiquement les colonnes du DataGrid en fonction du dictionnaire défini
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

        // Événement pour le bouton d'édition d'un client sélectionné
        private void Button_EditCustomer(object sender, RoutedEventArgs e)
        {
            // Navigue vers la vue d'édition du client avec le client sélectionné
            Main.Content = new EditCustomerView(SelectedUser);
        }

        // Événement pour le bouton d'ajout d'un nouveau client
        private void Button_AddCustomer(object sender, RoutedEventArgs e)
        {
            // Navigue vers la vue d'ajout de client
            Main.Content = new AddCustomerView();
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

        // Événement pour le bouton de suppression d'un client sélectionné
        private void Button_RemoveCustomer(object sender, RoutedEventArgs e)
        {
            // Vérifie s'il y a un client sélectionné et si la collection n'est pas nulle
            if (this.alluser != null && SelectedUser != null)
            {
                // Supprime le client sélectionné de la base de données et met à jour la collection
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
