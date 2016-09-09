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

namespace Seeker
{
    public partial class MainWindow : Window
    {
        Database db;
        // main window ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MainWindow()
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Employer Registration//////////////opend dialog window/////////////////////////////////////////////////////////////////////////
        private void btnERegister_Click(object sender, RoutedEventArgs e)
        {
            EmployerRegistration dialog = new EmployerRegistration();
            dialog.ShowDialog();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Job Seeker Registration///////////open dialog window///////////////////////////////////////////////////////////////////////////
        private void btnJSRegister_Click(object sender, RoutedEventArgs e)
        {
            JobSeekerRegistration dialog = new JobSeekerRegistration();
            dialog.ShowDialog();

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Login for Employers //////////////////////open the home page upon successfull login///////////////////////////////////////////
        private void btnELogIn_Click(object sender, RoutedEventArgs e)
        {
            string eEmail = tbEEmailAddress.Text;

            string eTryPassword = tbEPassword.Text;

            if (!db.GetEmployerByEmail(eEmail))
            {
                MessageBox.Show("You are not yet registered - Unicorn & glitter 4EVA - thug life and stuff - Toronto is overrated");
                return;
            }

            else if (db.GetEmployerByEmail(eEmail))
            {
                if (Globals.CurrentEmployer.EPassword != eTryPassword)
                {
                    MessageBox.Show("WRONG PASSWORD, good luck remembering it! - Unicorn & glitter 4EVA - thug life and stuff - Toronto is overrated");
                    return;
                }
                else
                {
                    EmployerHomePage dialog = new EmployerHomePage();
                    dialog.ShowDialog();
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Login for Job Seekers //////////////open the home page upon successfull login///////////////////////////////////////////////////
        private void btnJSLogIn_Click(object sender, RoutedEventArgs e)
        {
            string jsEmail = tbJSEmailAddress.Text;

            string jsTryPassword = tbJSPassword.Text;

            if (!db.GetJobSeekerByEmail(jsEmail))
            {
                MessageBox.Show("You are not yet registered - Unicorn & glitter 4EVA - thug life and stuff - Toronto is overrated");
                return;
            }

            else if (db.GetJobSeekerByEmail(jsEmail))
            {
                if (Globals.CurrentJobSeeker.JSPassword != jsTryPassword)
                {
                    MessageBox.Show("WRONG PASSWORD, good luck remembering it! - Unicorn & glitter 4EVA - thug life and stuff - Toronto is overrated");
                    return;
                }
                else
                {
                    JobSeekerHomePage dialog = new JobSeekerHomePage();
                    dialog.ShowDialog();
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
