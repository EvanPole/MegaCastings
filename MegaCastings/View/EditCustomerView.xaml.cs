using MegaCastings.DBLib.Class;
using System;
using System.Collections.Generic;
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

        public User User
        {
            get { return _User; }
            set { _User = value; }
        }

        public EditCustomerView(User user)
        {
            InitializeComponent();
            this.User = user;
            this.Prenom.Text = user.Firstname;
            this.Nom.Text = user.Lastname;
            this.Email.Text = user.Email;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = Prenom.Text;
            string lastName = Nom.Text;
            string eMail = Email.Text;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(eMail))
            {
                User.Lastname = firstName;
                User.Firstname = lastName;
                User.Email = eMail;


                using (MegaProductionContext context = new MegaProductionContext())
                {
                    context.Users.Update(User);
                    context.SaveChanges();
                }

                MessageBox.Show("Utilisateur update avec succès.");
                Main.Content = new CustomerView();
            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom et le nom de l'utilisateur.");
            }
        }
    }
}
