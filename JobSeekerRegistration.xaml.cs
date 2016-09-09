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

    public partial class JobSeekerRegistration : Window
    {
        Database db;
        public JobSeekerRegistration()
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

        // job Seeker registration event handler and verification //////////////////////////////
        private void btnRegisterJobSeeker_Click(object sender, RoutedEventArgs e)
        {
            // verification for the first name, must be at least 2 letters long
            string firstName = tbJSFirstName.Text;
            if (firstName.Length < 2)
            {
                MessageBox.Show("First name must be at least 2 letters long", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // verification for the Last name, must be at least 2 letters long
            string lastName = tbJSLastName.Text;
            if (firstName.Length < 2)
            {
                MessageBox.Show("Last name must be at least 2 letters long", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate email verification
            string email = tbJSEmail.Text;

            Regex emailVerification = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!emailVerification.Match(email).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: StarKidPotter@Hogwarts.uk", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate the phone number
            string phone = tbJSPhone.Text;

            Regex phoneVerification = new Regex(@"^[0-9]{10}$");
            if (!phoneVerification.Match(phone).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: 5145559999", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate that both password entered match
            string password1 = tbJSPassword.Text;
            string password2 = tbJSConfirmPassword.Text;

            if (password1 != password2)
            {
                MessageBox.Show("The passwords entered do not match", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate that there is at least some kind of entry in the Education text box
            string education = tbEducation.Text;

            if (education.Length < 15)
            {
                MessageBox.Show("Please give us some details about your education", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // validate that there is at least some kind of entry in the Experience text box
            string experience = tbExperience.Text;

            if (experience.Length < 15)
            {
                MessageBox.Show("Please give us some details about your previous or current work experience", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // create a new Job Seeker
            JobSeeker jobseeker = new JobSeeker { JSFirstName = firstName, JSLastName = lastName, JSEmail = email, JSPassword = password1, JSEducation = education, JSsExperience = experience, JSPhone = phone };

            // call the method in the database class to add the newly acquired data into the database
            try
            {
                if (db.RegisterJobSeeker(jobseeker))
                {
                    this.Close();
                    MessageBox.Show("Registration Successfull! You may now log in", "Registration Successfull!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We had an unexpected issue while trying to register your informations", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }
    }
}
