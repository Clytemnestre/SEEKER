using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Seeker
{

    public partial class EmployerRegistration : Window
    {
        Database db;
        public EmployerRegistration()
        {
            try
            {
                db = new Database();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fatal error, unable to connect to the database", "FATAL ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                throw e;
            }
            InitializeComponent();
        }

        // employer registration event handler and verification //////////////////////////////
        private void btnRegisterEmployer_Click(object sender, RoutedEventArgs e)
        {
            // verification for the name of the company, must be at least 3 letters long
            string nameOfCie = tbENameOfCie.Text;
            if (nameOfCie.Length < 3)
            {
                MessageBox.Show("The name of the company must be at least 3 letters long", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate email verification
            string eEmail = tbEEmailAddress.Text;

            Regex emailVerification = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!emailVerification.Match(eEmail).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: StarKidPotter@Hogwarts.uk", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate the phone number
            string ePhone = tbEPhoneNumber.Text;

            Regex phoneVerification = new Regex(@"^[0-9]{10}$");
            if (!phoneVerification.Match(ePhone).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: 5145559999", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate that both password entered match
            string password1 = tbEChoosenPassword.Text;
            string password2 = tbEChoosenPasswordConfirm.Text;

            if (password1 != password2)
            {
                MessageBox.Show("The passwords entered do not match", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // create a new Employer Reference

            Employer employer = new Employer { NameOfCompany = nameOfCie, EEmail = eEmail, EPhone = ePhone, EPassword = password1 };

            // call the method in the database class to add the newly acquired data into the database
            try
            {
                if (db.RegisterEmployer(employer))
                {
                    this.Close();
                    MessageBox.Show("Registration Successfull! You may now log in", "Registration Successfull!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We had an unexpected issue trying to register you", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }


    }
}
