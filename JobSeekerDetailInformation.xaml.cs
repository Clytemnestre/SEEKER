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
using System.Windows.Shapes;

namespace Seeker
{
    public partial class JobSeekerDetailInformation : Window
    {
        public JobSeekerDetailInformation()
        {
            InitializeComponent();
            tbFullName.Text = Globals.CurrentJobSeeker.JSFirstName + " " + Globals.CurrentJobSeeker.JSLastName;
            tbPhoneNumber.Text = Globals.CurrentJobSeeker.JSPhone;
            tbEmail.Text = Globals.CurrentJobSeeker.JSEmail;
            tbEducation.Text = Globals.CurrentJobSeeker.JSEducation;
            tbExperience.Text = Globals.CurrentJobSeeker.JSsExperience;
        }

        // print the detail information
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            // declare and initialize print dialog
            PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

            // hide the print button and print the window if the option is choosen
            if (printDlg.ShowDialog() == true)
            {
                // hide the button before printing it
                btnPrint.Visibility = Visibility.Hidden;
                printDlg.PrintVisual(this, "WPF Print");
            }
        }

    }
}