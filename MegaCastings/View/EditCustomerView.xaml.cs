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
    /// Interaction logic for EditCustomerView.xaml
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

        public EditCustomerView(User user)
        {
            this.User = user;
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
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory)
                {
                    GetSubCategoriesForSelectedBigCategory(selectedBigCategory);
                    this.DropdownSubCategory.ItemsSource = SubCategories;
                }

                this.checkboxisactive.IsChecked = user.Isactive == 1 ? true : false;
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

                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory && DropdownSubCategory.SelectedItem is SubCategory selectedSubCategory)
                {
                    User.Lastname = lastName;
                    User.Firstname = firstName; 
                    User.Email = eMail;
                    User.Birthdate = selectedDate.Value;
                    User.Bigcategoryid = selectedBigCategory.Id;
                    User.Subcategoryid = selectedSubCategory.Id;
                    User.Isactive = CheckBoxIsActive;

                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Users.Update(User);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Utilisateur update avec succès.");

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
                MessageBox.Show("Veuillez saisir le prénom et le nom de l'utilisateur.");
            }
        }
    }
}