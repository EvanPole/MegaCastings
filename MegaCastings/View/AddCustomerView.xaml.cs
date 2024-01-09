using MegaCastings.View;
using MegaCastings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MegaCastings.DBLib.Class;

namespace MegaCastings.View
{
    public partial class AddCustomerView : Page
    {
        public ObservableCollection<User> AllUsers { get; set; }
        public ObservableCollection<BigCategory> BigCategories { get; set; }
        public ObservableCollection<SubCategory> SubCategories { get; set; }

        public User SelectedUser { get; set; }

        public AddCustomerView()
        {
            InitializeComponent();
            DataContext = this; // Définition du DataContext sur cette instance de la classe
            BigCategories = GetBigCategories();
            DropdownBigCategories.SelectionChanged += DropdownBigCategories_SelectionChanged;
        }

        private ObservableCollection<BigCategory> GetBigCategories()
        {
            // Obtenez les catégories depuis la base de données
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<BigCategory>(context.BigCategories.ToList());
            }
        }

        private void DropdownBigCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory)
            {
                GetSubCategoriesForSelectedBigCategory(selectedBigCategory);
                DropdownSubCategory.ItemsSource = SubCategories;
            }
        }



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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = prenom.Text;
            string lastName = nom.Text;
            string eMail = email.Text;
            DateTime? selectedDate = birthdate.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(eMail) && selectedDate.HasValue)
            {
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory &&
                    DropdownSubCategory.SelectedItem is SubCategory selectedSubCategory)
                {
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

                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Users.Add(newUser);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Utilisateur ajouté avec succès.");
                    CustomerView CustomerView = new CustomerView();
                    NavigationService?.RemoveBackEntry(); 
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
