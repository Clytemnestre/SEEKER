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
    public partial class OfferInformation : Window
    {
        Database db;
        public OfferInformation()
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
            tbTitle.Text = Globals.CurrentOffer.OfferTitle;
            tbDescription.Text = Globals.CurrentOffer.OfferDescription;
        }

        private void btnUpdateOffer_Click(object sender, RoutedEventArgs e)
        {
            string title = tbTitle.Text;
            string description = tbDescription.Text;
            try
            {
                if (db.UpdateOfferById(title, description))
                {
                    MessageBox.Show("Your offer was updated", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We were unable to update the offer", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}
