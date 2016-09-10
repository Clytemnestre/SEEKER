using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Seeker
{
    public partial class JobSeekerHomePage : Window
    {
        Database db;
        public JobSeekerHomePage()
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
            tbEducation1.Text = Globals.CurrentJobSeeker.JSEducation;
            tbExperience.Text = Globals.CurrentJobSeeker.JSsExperience;

        }

        // enable the education text box when the update button is clicked
        private void btnUpdateEducation_Click(object sender, RoutedEventArgs e)
        {
            tbEducation1.IsEnabled = true;
        }

        // save education information event handler
        private void btnSaveEducation_Click(object sender, RoutedEventArgs e)
        {
            string updatedEducation = tbEducation1.Text;
            try
            {
                if (db.UpdateEducationByID(updatedEducation))
                {
                    tbEducation1.IsEnabled = false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We had an unexpected issue while trying to register your informations", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }

        // enable the experience button
        private void btnUpdateExperience_Click(object sender, RoutedEventArgs e)
        {
            tbExperience.IsEnabled = true;
        }

        // save experience information event handler
        private void btnSaveExperience_Click(object sender, RoutedEventArgs e)
        {
            string updatedExperience = tbExperience.Text;
            try
            {
                if (db.UpdateEducationByID(updatedExperience))
                {
                    tbEducation1.IsEnabled = false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We had an unexpected issue while trying to register your informations", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }

        // event handler to show the fields to change passwords
        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            lblNewPassword.Visibility = Visibility.Visible;
            lblConfirmNewPassword.Visibility = Visibility.Visible;
            tbNewPassword.Visibility = Visibility.Visible;
            tbConfirmNewPassword.Visibility = Visibility.Visible;
        }


    }
}
