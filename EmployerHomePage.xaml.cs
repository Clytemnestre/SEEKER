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
    public partial class EmployerHomePage : Window
    {
        Database db;
        public EmployerHomePage()
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
            tbNameOfCompany.Text = Globals.CurrentEmployer.NameOfCompany;
            tbEmail.Text = Globals.CurrentEmployer.EEmail;
            tbPhoneNumber.Text = Globals.CurrentEmployer.EPhone;

            // load the job offer belonging to the current employer into the datagrid inside the applicants & offer tab
            try
            {
                List<Offer> listOfOffers = db.GetOffersByemployerID(Globals.CurrentEmployer.EID);
                dgJobOffers.ItemsSource = listOfOffers;
                dgOffers.ItemsSource = listOfOffers;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Could not load your offers", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                return; ;
            }

        }
        // applications tab

        // load the applicant when the job offer is selected
        private void dgJobOffers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Offer o = (Offer)dgJobOffers.SelectedItem;
            int offerID = o.OfferID;
            List<int> listOfJobSeekerID = new List<int>();
            try
            {
                listOfJobSeekerID = db.GetApplicationByOfferID(offerID);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We were unable to load the applicants", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            if (listOfJobSeekerID == null)
            {
                MessageBox.Show("No one has applied for this posting", "NO ONE WANTS YOUR SHITTY JOB", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            List<JobSeeker> listOfJobSeekers = db.GetJobSeekerByID(listOfJobSeekerID);
            dgApplicants.ItemsSource = listOfJobSeekers;
            dgApplicants.ItemsSource = listOfJobSeekers;
        }

        // Offer tab - enable buttons upon selection of an offer
        private void dgOffers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteOffer.IsEnabled = true;
            btnModifyOffer.IsEnabled = true;
        }

        // delete the selected offer
        private void btnDeleteOffer_Click(object sender, RoutedEventArgs e)
        {
            // confirmation of deletion
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected record", "Action", MessageBoxButton.YesNo, MessageBoxImage.Question);

            Offer o = (Offer)dgOffers.SelectedItem;

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Offer offer = (Offer)dgOffers.SelectedItem;
                    int id = offer.OfferID;
                    try
                    {
                        db.DeleteOfferByID(id);
                        List<Offer> listOfOffers = db.GetOffersByemployerID(Globals.CurrentEmployer.EID);
                        dgJobOffers.ItemsSource = listOfOffers;
                        dgOffers.ItemsSource = listOfOffers;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("We were unable to delete your offer", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }

        }

        // modify the current offer
        private void btnModifyOffer_Click(object sender, RoutedEventArgs e)
        {
            Offer offer = (Offer)dgOffers.SelectedItem;
            Globals.CurrentOffer = offer;
            OfferInformation dialog = new OfferInformation();
            dialog.Show();
        }

        // Open a window to create anew offer
        private void btnNewOffer_Click(object sender, RoutedEventArgs e)
        {
            CreateNewOffer dialog = new CreateNewOffer();
            dialog.Show();
        }

        private void btnUpdateAccountInformation_Click(object sender, RoutedEventArgs e)
        {
            // verification of the name of the company
            string nameOfCompany = tbNameOfCompany.Text;
            if (nameOfCompany.Length < 3)
            {
                MessageBox.Show("The name of the company must be at least 3 letters long", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            // verification of the email
            string email = tbEmail.Text;
            Regex emailVerification = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!emailVerification.Match(email).Success)
            {
                MessageBox.Show("Please enter an email address in a valid format ex: StarKidPotter@Hogwarts.uk", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // verification of the phone number
            string phoneNumber = tbPhoneNumber.Text;
            Regex phoneVerification = new Regex(@"^[0-9]{10}$");
            if (!phoneVerification.Match(phoneNumber).Success)
            {
                MessageBox.Show("Please enter your phone number in a valid format ex: 5145559999", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (db.UpdateEmployerAccountByID(nameOfCompany, email, phoneNumber))
                {
                    MessageBox.Show("Your account information had been updated", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    tbNameOfCompany.Text = Globals.CurrentEmployer.NameOfCompany;
                    tbEmail.Text = Globals.CurrentEmployer.EEmail;
                    tbPhoneNumber.Text = Globals.CurrentEmployer.EPhone;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We were unable to update your account information", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        // update the password
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // verify that the current password entered is valid
            string currentPassword = tbCurrentPassword.Text;
            if (currentPassword != Globals.CurrentEmployer.EPassword)
            {
                MessageBox.Show("You did not provide the correct current password", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // new passwords verifications
            string password1 = tbNewPassword.Text;
            string password2 = tbConfirmNewPassword.Text;
            // validate that the new password has at least 5 characters
            if (password1.Length < 5)
            {
                MessageBox.Show("Password must be at least 5 letters long", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // validate that both password entered match
            if (password1 != password2)
            {
                MessageBox.Show("The passwords entered do not match", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (db.UpdateEmployerPasswordByID(password1))
                {
                    MessageBox.Show("Your password has been updated", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    tbCurrentPassword.Text = "";
                    tbNewPassword.Text = "";
                    tbConfirmNewPassword.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We were unable to update your password", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


        }

        // enable the see applicant details button 
        private void dgApplicants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            seeApplicantDetails.IsEnabled = true;
        }

        // open a new window with the applicants information inside. 
        private void seeApplicantDetails_Click(object sender, RoutedEventArgs e)
        {
            JobSeeker jobSeeker = (JobSeeker)dgApplicants.SelectedItem;
            Globals.CurrentJobSeeker.JSFirstName = jobSeeker.JSFirstName;
            Globals.CurrentJobSeeker.JSLastName = jobSeeker.JSLastName;
            Globals.CurrentJobSeeker.JSPhone = jobSeeker.JSPhone;
            Globals.CurrentJobSeeker.JSEmail = jobSeeker.JSEmail;
            Globals.CurrentJobSeeker.JSEducation = jobSeeker.JSEducation;
            Globals.CurrentJobSeeker.JSsExperience = jobSeeker.JSsExperience;
            JobSeekerDetailInformation dialog = new JobSeekerDetailInformation();
            dialog.ShowDialog();
        }
    }
}