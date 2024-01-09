using MegaCastings.DBLib.Class;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MegaCastings.View
{
    /// <summary>
    /// Représente la logique de l'interface utilisateur pour l'édition d'un client.
    /// </summary>
    public partial class EditCustomerView : Page
    {
        private User _User;
        public ObservableCollection<BigCategory>? BigCategories { get; set; }
        public ObservableCollection<SubCategory>? SubCategories { get; set; }

        public User User
        {
            get { return _User; }
            set { _User = value; }
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
        /// Événement déclenché lors du changement de sélection de la catégorie principale.
        /// </summary>
        private void DropdownBigCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory)
            {
                GetSubCategoriesForSelectedBigCategory(selectedBigCategory);
                DropdownSubCategory.ItemsSource = SubCategories;
            }
        }

        /// <summary>
        /// Obtient les sous-catégories pour la catégorie principale sélectionnée.
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
        /// Initialise une nouvelle instance de la classe <see cref="EditCustomerView"/>.
        /// </summary>
        public EditCustomerView(User user)
        {
            this.User = user;

            // Vérifie si l'utilisateur est null (nouvelle édition) et redirige vers la vue des clients
            if (user == null)
            {
                CustomerView CustomerView = new CustomerView();
                NavigationService?.RemoveBackEntry();
                NavigationService?.Navigate(CustomerView);
            }
            else
            {
                InitializeComponent();
                DataContext = this;
                this.prenom.Text = user.Firstname;
                this.nom.Text = user.Lastname;
                this.email.Text = user.Email;
                this.birthdate.SelectedDate = user.Birthdate;

                this.DropdownBigCategories.ItemsSource = GetBigCategories();
                DropdownBigCategories.SelectionChanged += DropdownBigCategories_SelectionChanged;

                // Vérifie la sélection de la catégorie principale et met à jour les sous-catégories
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory)
                {
                    GetSubCategoriesForSelectedBigCategory(selectedBigCategory);
                    this.DropdownSubCategory.ItemsSource = SubCategories;
                }

                this.checkboxisactive.IsChecked = user.Isactive == 1 ? true : false;
            }
        }

        /// <summary>
        /// Événement déclenché lors du clic sur le bouton de sauvegarde des modifications de l'utilisateur.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = prenom.Text;
            string lastName = nom.Text;
            string eMail = email.Text;
            DateTime? selectedDate = birthdate.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;

            // Vérifie si les champs obligatoires sont remplis
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(eMail) && selectedDate.HasValue)
            {
                // Vérifie la sélection de la catégorie principale et de la sous-catégorie
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory && DropdownSubCategory.SelectedItem is SubCategory selectedSubCategory)
                {
                    // Met à jour les propriétés de l'utilisateur avec les nouvelles données
                    User.Lastname = lastName;
                    User.Firstname = firstName;
                    User.Email = eMail;
                    User.Birthdate = selectedDate.Value;
                    User.Bigcategoryid = selectedBigCategory.Id;
                    User.Subcategoryid = selectedSubCategory.Id;
                    User.Isactive = CheckBoxIsActive;

                    // Met à jour l'utilisateur dans la base de données
                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Users.Update(User);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Utilisateur mis à jour avec succès.");

                    // Redirige vers la vue des clients
                    CustomerView CustomerView = new CustomerView();
                    NavigationService?.RemoveBackEntry(); // Efface l'historique de navigation
                    NavigationService?.Navigate(CustomerView);
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une catégorie principale et une sous-catégorie.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom et le nom de l'utilisateur.");
            }
        }
    }
}
