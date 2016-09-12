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
            // load the job offer belonging to the current employer into the datagrid inside the applicants & offer tab
            try
            {
                List<Offer> listOfOffers = db.GetOffersByemployerID(Globals.CurrentEmployer.EID);
                dgJobOffers.ItemsSource = listOfOffers;
                dgOffers.ItemsSource = listOfOffers;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Could not load your offers", "FATAL ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                throw ex;
            }

        }
        // application tab - enable buttons upon selection of an offer
        private void dgOffers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteOffer.IsEnabled = true;
            btnModifyOffer.IsEnabled = true;
            /*Offer offer = (Offer)dgOffers.SelectedItem;
            Globals.CurrentOffer = offer;
            MessageBox.Show(Globals.CurrentOffer.OfferDescription);*/
        }

        // delete the selected offer
        private void btnDeleteOffer_Click(object sender, RoutedEventArgs e)
        {
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
            MessageBox.Show(Globals.CurrentOffer.OfferDescription);
            OfferInformation dialog = new OfferInformation();
            dialog.Show();
        }

        private void btnNewOffer_Click(object sender, RoutedEventArgs e)
        {
            CreateNewOffer dialog = new CreateNewOffer();
            dialog.Show();
        }
        



    }
}
