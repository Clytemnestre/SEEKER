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
            tbaFirstName.Text = Globals.CurrentJobSeeker.JSFirstName;
            tbaLastName.Text = Globals.CurrentJobSeeker.JSLastName;
            tbaEmail.Text = Globals.CurrentJobSeeker.JSEmail;
            tbAccountPhoneNumber.Text = Globals.CurrentJobSeeker.JSPhone;
            tbaPassword.Text = Globals.CurrentJobSeeker.JSPassword;

            // load the previously sent out applications with a GetApplicationsByJobSeekerID 
            try
            {
                List<int> listOfOfferID = db.GetApplicationsByJobSeekerID(Globals.CurrentJobSeeker.JSID);
                List<Offer> listOfOffers = db.GetOfferById(listOfOfferID);
                dgDisplayApplicationHistory.ItemsSource = listOfOffers;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Could not load your applications", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                throw ex;
            }

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

        // enable the update experience button
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
                if (db.UpdateExperienceByID(updatedExperience))
                {
                    tbExperience.IsEnabled = false;
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
            btnUpdatePassword.Visibility = Visibility.Visible;
        }

        // Event handler to update the account information
        private void btnUdateAccountInformationByID_Click(object sender, RoutedEventArgs e)
        {
            string firstName = tbaFirstName.Text;
            string lastName = tbaLastName.Text;
            string email = tbaEmail.Text;
            string phoneNumber = tbAccountPhoneNumber.Text;
            // verify the validity of the new data
            // First name
            if (firstName.Length < 2)
            {
                MessageBox.Show("First name must be at least two letters long", "Impossible to update your account information", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Last name
            if (lastName.Length < 2)
            {
                MessageBox.Show("Last name must be at least two letters long", "Impossible to update your account information", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // email address
            Regex emailVerification = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!emailVerification.Match(email).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: StarKidPotter@Hogwarts.uk", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // phone number
            Regex phoneVerification = new Regex(@"^[0-9]{10}$");
            if (!phoneVerification.Match(phoneNumber).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: 5145559999", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // call to the method UpdateAccountInformationById() in the database class
            try
            {
                if (db.UpdateAccountInformationById(firstName, lastName, email, phoneNumber))
                {
                    MessageBox.Show("Your account was successfully updated", "Account updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We were unable to update your account information", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // Event handler to modify the password
        private void btnUdateAccountInformation_Click(object sender, RoutedEventArgs e)
        {
            string password1 = tbNewPassword.Text;
            string password2 = tbConfirmNewPassword.Text;

            if (password1.Length < 5)
            {
                MessageBox.Show("Your password must at least be 5 characters long", "invalid password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (password1 == password2)
            {
                try
                {
                    if (db.UpdatePasswordById(password1))
                    {
                        MessageBox.Show("Your password was updated", "Password Update", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        lblNewPassword.Visibility = Visibility.Hidden;
                        lblConfirmNewPassword.Visibility = Visibility.Hidden;
                        tbNewPassword.Visibility = Visibility.Hidden;
                        tbConfirmNewPassword.Visibility = Visibility.Hidden;
                        btnUpdatePassword.Visibility = Visibility.Hidden;

                        // change the password field to mirror the proper value.
                        tbaPassword.Text = password1;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("We were unable to update your password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

        }

        // Event handler for the search button
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            string searchTerm = tbSearch.Text;
            if (searchTerm.Length < 3)
            {
                MessageBox.Show("Please enter a valid search term. EX: 'Nurse'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // call a method SearchByTerm inside the database class and load the results inside the datagrid (binding)
            try
            {
                List<Offer> listOfOffers = db.SearchByTerm(searchTerm);
                 dgDisplaySearchResult.ItemsSource = listOfOffers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to retireve Offers");
            }
        }

        // see the details of the selected Job offer
        private void dgDisplaySearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Offer o = (Offer)dgDisplaySearchResult.SelectedItem;
            int employerID = o.EmployerID;
            // get the name of the company that posted the offer by employerID
            string companyName = "";
            try
            {
                companyName = db.GetCompanyNameByID(employerID);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("unable to retireve the name of the company");
                return;
            }
            tbOfferID.Text = o.OfferID.ToString();
            tbOfferTitle.Text = o.OfferTitle;
            tbNameOfCompany.Text = companyName;
            tbDescriptionOfJob.Text = o.OfferDescription;

        }

        // apply on a job offer
        private void btnApplyOnOffer_Click(object sender, RoutedEventArgs e)
        {
            int offerID = int.Parse(tbOfferID.Text);
            try
            {
                // the first step is to check that the job seeker has not already applied to this job
                if (db.VerifyApplicationByID(offerID, Globals.CurrentJobSeeker.JSID))
                {
                    try
                    {
                        // second step - add the application
                        if (db.ApplyOntoAnOfferByID(offerID, Globals.CurrentJobSeeker.JSID))
                        {
                            // reload the history of application so that the info in the "applied for" tab is correct
                            try
                            {
                                List<int> listOfOfferID = db.GetApplicationsByJobSeekerID(Globals.CurrentJobSeeker.JSID);
                                List<Offer> listOfOffers = db.GetOfferById(listOfOfferID);
                                dgDisplayApplicationHistory.ItemsSource = listOfOffers;
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show("Could not load your applications", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                                throw ex;
                            }
                            MessageBox.Show("Your offer was received");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("We were unable to send your offer");
                    }
                }
                else if (!db.VerifyApplicationByID(offerID, Globals.CurrentJobSeeker.JSID))
                {
                    MessageBox.Show("You have already applied on this offer");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("unable to make the appropriate verifications");
                return;
            }
        }

        // see the detail of the offer on which you applied
        private void dgDisplayApplicationHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Offer o = (Offer)dgDisplayApplicationHistory.SelectedItem;
            int employerID = o.EmployerID;
            // get the name of the company that posted the offer by employerID  /// CODE REPETITION, COULD/SHOULD CREATE A METHOD - running out of time.
            string companyName = "";
            try
            {
                companyName = db.GetCompanyNameByID(employerID);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("unable to retireve the name of the company");
                return;
            }
            tbIDOfOffer.Text = o.OfferID.ToString();
            tbTitleOffer.Text = o.OfferTitle;
            tbNameOfTheCompany.Text = companyName;
            tbJobDescription.Text = o.OfferDescription;
        }    
    }
}

