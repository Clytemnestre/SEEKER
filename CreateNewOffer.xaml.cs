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
    /// <summary>
    /// Interaction logic for CreateNewOffer.xaml
    /// </summary>
    public partial class CreateNewOffer : Window
    {
        Database db;
        public CreateNewOffer()
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

        private void btnCreateOffer_Click(object sender, RoutedEventArgs e)
        {
            string title = tbTitle.Text;
            string description = tbDescription.Text;
            int employerID = Globals.CurrentEmployer.EID;
            /*try
            {
                if (*/db.CreateOffer(title, description, employerID);/*))
                {
                    MessageBox.Show("Your offer was created", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We were unable to create your offer", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }*/
        }
    }
}
