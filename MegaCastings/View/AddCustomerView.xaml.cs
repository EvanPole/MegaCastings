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
using MegaCastings.DBLib.Class;

namespace MegaCastings.View
{
    /// <summary>
    /// Représente la logique de l'interface utilisateur pour l'ajout d'un nouveau client.
    /// </summary>
    public partial class AddCustomerView : Page
    {
        // Collections pour contenir tous les utilisateurs, les catégories principales et les sous-catégories
        public ObservableCollection<User> AllUsers { get; set; }
        public ObservableCollection<BigCategory> BigCategories { get; set; }
        public ObservableCollection<SubCategory> SubCategories { get; set; }

        // Utilisateur actuellement sélectionné
        public User SelectedUser { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="AddCustomerView"/>.
        /// </summary>
        public AddCustomerView()
        {
            // Initialise les composants de l'interface utilisateur
            InitializeComponent();

            // Définit le contexte de données pour la liaison de données
            DataContext = this;

            // Récupère les catégories principales et initialise l'événement de changement de sélection
            BigCategories = GetBigCategories();
            DropdownBigCategories.SelectionChanged += DropdownBigCategories_SelectionChanged;
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
        /// Événement déclenché lors du changement de sélection dans la catégorie principale.
        /// </summary>
        private void DropdownBigCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Met à jour les sous-catégories en fonction de la catégorie principale sélectionnée
            if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory)
            {
                GetSubCategoriesForSelectedBigCategory(selectedBigCategory);
                DropdownSubCategory.ItemsSource = SubCategories;
            }
        }

        /// <summary>
        /// Obtient les sous-catégories pour une catégorie principale sélectionnée depuis la base de données.
        /// </summary>
        private void GetSubCategoriesForSelectedBigCategory(BigCategory selectedBigCategory)
        {
            using (MegaProductionContext context = new MegaProductionContext())
            {
                SubCategories = new ObservableCollection<SubCategory>(
                    context.SubCategories
                           .Where(sub => sub.Bigcategoryid == selectedBigCategory.Id)
                           .ToList());

                // Mettre à jour l'ItemsSource de DropdownSubCategory
                DropdownSubCategory.ItemsSource = SubCategories;
            }
        }

        /// <summary>
        /// Événement déclenché lors du clic sur le bouton d'ajout d'un nouvel utilisateur (client).
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Récupère les valeurs saisies par l'utilisateur
            string firstName = prenom.Text;
            string lastName = nom.Text;
            string eMail = email.Text;
            DateTime? selectedDate = birthdate.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;

            // Vérifie si les champs obligatoires sont remplis
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(eMail) && selectedDate.HasValue)
            {
                // Vérifie la sélection de la catégorie principale et de la sous-catégorie
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory &&
                    DropdownSubCategory.SelectedItem is SubCategory selectedSubCategory)
                {
                    // Crée un nouvel utilisateur avec les données saisies
                    User newUser = new User
                    {
                        Lastname = lastName,
                        Firstname = firstName,
                        Email = eMail,
                        Birthdate = selectedDate.Value,
                        Bigcategoryid = selectedBigCategory.Id,
                        Subcategoryid = selectedSubCategory.Id,
                        Isactive = CheckBoxIsActive
                    };

                    // Ajoute le nouvel utilisateur à la base de données
                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Users.Add(newUser);
                        context.SaveChanges();
                    }

                    // Affiche un message de succès
                    MessageBox.Show("Utilisateur ajouté avec succès.");

                    // Redirige vers la vue des clients
                    CustomerView CustomerView = new CustomerView();
                    NavigationService?.RemoveBackEntry(); // Efface l'historique de navigation
                    NavigationService?.Navigate(CustomerView);
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une catégorie et une sous-catégorie.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom, le nom et sélectionner une date de naissance pour l'utilisateur.");
            }
        }
    }
}
