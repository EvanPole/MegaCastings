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
    /// Logique d'interaction pour AddPartnerView.xaml
    /// </summary>
    public partial class AddPartnerView : Page
    {
        public AddPartnerView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = prenom.Text;
            string lastName = nom.Text;
            string eMail = email.Text;


            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(eMail))
            {
                User newUser = new User
                {
                    Lastname = firstName,
                    Firstname = lastName,
                    Email = eMail,
                };

                using (MegaProductionContext context = new MegaProductionContext())
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Utilisateur ajouté avec succès.");
                Main.Content = new CustomerView();
            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom et le nom de l'utilisateur.");
            }
        }
    }
}
