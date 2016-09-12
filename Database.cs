using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Seeker
{
    // Employer class ///////////////////////////////////////////////////////////////////////////////////////////////////
    class Employer
    {
        public int EID {get; set;}
        public string NameOfCompany { get; set; }
        public string EEmail { get; set; }
        public string EPhone { get; set; }
        public string EPassword { get; set; }

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // JobSeeker class //////////////////////////////////////////////////////////////////////////////////////////////////
    class JobSeeker
    {
        public int JSID { get; set; }
        public string JSFirstName { get; set; }
        public string JSLastName { get; set; }
        public string JSEmail { get; set; }
        public string JSPassword { get; set; }
        public string JSEducation { get; set; }
        public string JSsExperience { get; set; }
        public string JSPhone { get; set; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Offer class //////////////////////////////////////////////////////////////////////////////////////////////////////
    class Offer
    {
        public int OfferID { get; set; }
        public string OfferTitle { get; set; }
        public string OfferDescription { get; set; }
        public int EmployerID { get; set; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Database class ///////////////////////////////////////////////////////////////////////////////////////////////////
    class Database
    {
        public int JOBSEEKERID = 0;
        const string CONN_STRING = @"Data Source=ipd8vs.database.windows.net;Initial Catalog=seeker;Integrated Security=False;User ID=sqladmin;Password=IPD8rocks!;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection conn;
        // open the connection to the database
        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        } 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////LOGIN & REGISTER FOR JOB SEEKERS AND EMPLOYERS///////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Login for employers ////////////////////////////////////////////////////////////////////////////////////////////     
        public bool GetEmployerByEmail(string eEmail)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employers WHERE eEmail = @eEmail", conn);
            cmd.Parameters.AddWithValue("@eEmail", eEmail);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Globals.CurrentEmployer.EPassword = reader.GetString(reader.GetOrdinal("ePassword"));
                        Globals.CurrentEmployer.EID = reader.GetInt32(reader.GetOrdinal("eID"));
                        Globals.CurrentEmployer.NameOfCompany = reader.GetString(reader.GetOrdinal("NameOfCompany"));
                        Globals.CurrentEmployer.EEmail = reader.GetString(reader.GetOrdinal("eEmail"));
                        Globals.CurrentEmployer.EPhone = reader.GetString(reader.GetOrdinal("ephone"));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Login for Job Seekers ////////////////////////////////////////////////////////////////////////////////////////////     
        public bool GetJobSeekerByEmail(string jsEmail)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM JobSeekers WHERE jsEmail = @jsEmail", conn);
            cmd.Parameters.AddWithValue("@jsEmail", jsEmail);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Globals.CurrentJobSeeker.JSPassword = reader.GetString(reader.GetOrdinal("jsPassword"));
                        Globals.CurrentJobSeeker.JSID = reader.GetInt32(reader.GetOrdinal("jsID"));
                        Globals.CurrentJobSeeker.JSFirstName = reader.GetString(reader.GetOrdinal("jsFirstName"));
                        Globals.CurrentJobSeeker.JSLastName = reader.GetString(reader.GetOrdinal("jsLastName"));
                        Globals.CurrentJobSeeker.JSEmail = reader.GetString(reader.GetOrdinal("jsEmail"));
                        Globals.CurrentJobSeeker.JSPhone = reader.GetString(reader.GetOrdinal("jsPhone"));
                        Globals.CurrentJobSeeker.JSEducation = reader.GetString(reader.GetOrdinal("jsEducation"));
                        Globals.CurrentJobSeeker.JSsExperience = reader.GetString(reader.GetOrdinal("jsExperience"));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // register Employer ////////////////////////////////////////////////////////////////////////////////////////////// 
        public bool RegisterEmployer(Employer employer)
        {
            // find a way to warn if email is already in use
            // SQL
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Employers (nameOfCie, eEmail, ephone, ePassword) VALUES (@nameOfCie, @eEmail, @ephone, @ePassword)", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@nameOfCie", employer.NameOfCompany);
                cmd.Parameters.AddWithValue("@eEmail", employer.EEmail);
                cmd.Parameters.AddWithValue("@ephone", employer.EPhone);
                cmd.Parameters.AddWithValue("@ePassword", employer.EPassword);
                cmd.ExecuteNonQuery();
                return true;
            } 
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // register Job Seekers ///////////////////////////////////////////////////////////////////////////////////////////
        public bool RegisterJobSeeker(JobSeeker jobseeker)
        {
            // find a way to warn if email is already in use
            // SQL
            using (SqlCommand cmd = new SqlCommand("INSERT INTO JobSeekers (jsFirstName, jsLastName, jsEmail, jsPassword, jsPhone, jsEducation, jsExperience) VALUES (@jsFirstName, @jsLastName, @jsEmail, @jsPassword, @jsPhone, '', '')", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@jsFirstName", jobseeker.JSFirstName);
                cmd.Parameters.AddWithValue("@jsLastName", jobseeker.JSLastName);
                cmd.Parameters.AddWithValue("@jsEmail", jobseeker.JSEmail);
                cmd.Parameters.AddWithValue("@jsPassword", jobseeker.JSPassword);
                cmd.Parameters.AddWithValue("@jsPhone", jobseeker.JSPhone);
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////JOB SEEKER HOME PAGE////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PROFILE ////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Update the Education information ///////////////////////////////////////////////////////////////////////////////
        public bool UpdateEducationByID(string updatedEducation)
        {
            // SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE JobSeekers SET jsEducation = @jsEducation WHERE jsID = @jsID", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@jsEducation", updatedEducation);
                cmd.Parameters.AddWithValue("@jsID", Globals.CurrentJobSeeker.JSID);
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Update the Experience information //////////////////////////////////////////////////////////////////////////////
        public bool UpdateExperienceByID(string updatedExperience)
        {
            // SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE JobSeekers SET jsExperience = @jsExperience WHERE jsID = @jsID"))
            {
                MessageBox.Show("2");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@jsExperience", updatedExperience);
                cmd.Parameters.AddWithValue("@jsID", Globals.CurrentJobSeeker.JSID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("3");
                return true;
            }
        }

        // ACCOUNT ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Update the account information /////////////////////////////////////////////////////////////////////////////////
        public bool UpdateAccountInformationById(string firstName, string lastName, string email, string phoneNumber)
        {
            // SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE JobSeekers SET JSFirstName = @JSFirstName, JSLastName = @JSLastName, JSEmail = @JSEmail, JSPhone = @JSPhone  WHERE jsID = @jsID", conn))
            {
                MessageBox.Show("6");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@JSFirstName", firstName);
                cmd.Parameters.AddWithValue("@JSLastName", lastName); 
                cmd.Parameters.AddWithValue("@JSEmail", email);       
                cmd.Parameters.AddWithValue("@JSPhone", phoneNumber);
                cmd.Parameters.AddWithValue("@jsID", Globals.CurrentJobSeeker.JSID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("7");
                return true;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Update password ////////////////////////////////////////////////////////////////////////////////////////////////

        public bool UpdatePasswordById(string password)
        {
            //SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE JobSeekers SET JSPassword = @JSPassword WHERE jsID = @jsID", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@JSPassword", password);
                cmd.Parameters.AddWithValue("@jsID", Globals.CurrentJobSeeker.JSID);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        // JOB SEARCH /////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Search job offers by term //////////////////////////////////////////////////////////////////////////////////////
        public List <Offer> SearchByTerm(string term)
        {
            List<Offer> list = new List<Offer>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Offers WHERE OfferTitle LIKE '%" + term + "%'", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int offerId = reader.GetInt32(reader.GetOrdinal("OfferID"));
                        string title = reader.GetString(reader.GetOrdinal("OfferTitle"));
                        string description = reader.GetString(reader.GetOrdinal("OfferDescription"));
                        int employerId = reader.GetInt32(reader.GetOrdinal("EmployerID"));
                        Offer o = new Offer() { OfferID = offerId, OfferTitle = title, OfferDescription = description, EmployerID = employerId};
                        list.Add(o);
                    }
                }
            }
            return list;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////EMPLOYER HOME PAGE//////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // APPLICANTS /////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // load the job offer of the current employer /////////////////////////////////////////////////////////////////////
        public List<Offer> GetOffersByemployerID(int id)
        {
            List<Offer> list = new List<Offer>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Offers WHERE EmployerID = @EmployerID", conn);
            cmd.Parameters.AddWithValue("@EmployerID", id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int offerId = reader.GetInt32(reader.GetOrdinal("OfferID"));
                        string title = reader.GetString(reader.GetOrdinal("OfferTitle"));
                        string description = reader.GetString(reader.GetOrdinal("OfferDescription"));
                        int employerId = reader.GetInt32(reader.GetOrdinal("EmployerID"));
                        Offer o = new Offer() { OfferID = offerId, OfferTitle = title, OfferDescription = description, EmployerID = employerId };
                        list.Add(o);
                    }
                }
            }
            return list;
        }

        // OFFERS /////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Delete a job offer /////////////////////////////////////////////////////////////////////////////////////////////
        public void DeleteOfferByID(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Offers WHERE OfferID=@OfferID", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@OfferID", id);
                cmd.ExecuteNonQuery();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Update a job offer /////////////////////////////////////////////////////////////////////////////////////////////
        public bool UpdateOfferById(string title, string description)
        {
            //SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE Offers SET OfferTitle= @OfferTitle, OfferDescription = @OfferDescription WHERE OfferID = @OfferID", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@OfferTitle", title);
                cmd.Parameters.AddWithValue("@OfferDescription", description);
                cmd.Parameters.AddWithValue("@OfferID", Globals.CurrentOffer.OfferID);
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Create a job offer /////////////////////////////////////////////////////////////////////////////////////////////
        public bool CreateOffer(string title, string description, int employerID)
        {
            //SQL
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Offers (OfferTitle, OfferDescription, EmployerID) VALUES (@OfferTitle, @OfferDescription, @EmployerID)", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@OfferTitle", title);
                cmd.Parameters.AddWithValue("@OfferDescription", description);
                cmd.Parameters.AddWithValue("@EmployerID", Globals.CurrentOffer.OfferID);
                cmd.ExecuteNonQuery();
                return true;
            }

        }
    }
}


