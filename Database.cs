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
    public class Employer
    {
        public int EID {get; set;}
        public string NameOfCompany { get; set; }
        public string EEmail { get; set; }
        public string EPhone { get; set; }
        public string EPassword { get; set; }

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // JobSeeker class //////////////////////////////////////////////////////////////////////////////////////////////////
    public class JobSeeker
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
    public class Offer
    {
        public int OfferID { get; set; }
        public string OfferTitle { get; set; }
        public string OfferDescription { get; set; }
        public int EmployerID { get; set; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Application class //////////////////called volunteer because Application is a reserved word///////////////////////
    public class Volunteer
    {
        public int EmployerID { get; set; }
        public int JobSeekerID { get; set; }
        public int ApplicationID { get; set; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Database class ///////////////////////////////////////////////////////////////////////////////////////////////////
    public class Database
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
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Employers (NameOfCompany, eEmail, ephone, ePassword) VALUES (@NameOfCompany, @eEmail, @ephone, @ePassword)", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@NameOfCompany", employer.NameOfCompany);
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
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@jsExperience", updatedExperience);
                cmd.Parameters.AddWithValue("@jsID", Globals.CurrentJobSeeker.JSID);
                cmd.ExecuteNonQuery();
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
            // SQL
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
        // Load the information of the selected offer /////////////////////////////////////////////////////////////////////
        public string GetCompanyNameByID(int employerID)
        {
            string nameOfCompany = "";
            SqlCommand cmd = new SqlCommand("SELECT NameOfCompany FROM Employers WHERE EID = @EID", conn);
            cmd.Parameters.AddWithValue("@EID", employerID);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        nameOfCompany = reader.GetString(reader.GetOrdinal("NameOfCompany"));
                        
                    }
                }
            }
            return nameOfCompany;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Verify that the job seeker has not already applied on the choosen offer ////////////////////////////////////////
        public bool VerifyApplicationByID(int offerID, int jobSeekerID)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE OfferID = @OfferID AND JobSeekerID = @JobSeekerID", conn);
            cmd.Parameters.AddWithValue("@OfferID", offerID);
            cmd.Parameters.AddWithValue("@JobSeekerID", jobSeekerID);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    // if it finds something, it mean that the job seeker has already applied to this offer
                    return false;
                } 
                else 
                {
                    return true;
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Apply for an Offer /////////////////////////////////////////////////////////////////////////////////////////////
        public bool ApplyOntoAnOfferByID(int offerID, int jobSeekerID)
        {
            // SQL
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Applications (OfferID, JobSeekerID) VALUES (@OfferID, @JobSeekerID)", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@OfferID", offerID);
                cmd.Parameters.AddWithValue("@JobSeekerID", jobSeekerID);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        // APPLIED FOR /////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // get applications by job seeker id //////////////////////////////////////////////////////////////////////////////
        public List <int> GetApplicationsByJobSeekerID(int jobSeekerID)
        {
            List<int> list = new List<int>();
            // SQL
            SqlCommand cmd = new SqlCommand("SELECT OfferID FROM Applications WHERE JobSeekerID = @JobSeekerID", conn);
            cmd.Parameters.AddWithValue("@JobSeekerID", jobSeekerID);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int offerID = reader.GetInt32(reader.GetOrdinal("OfferID"));
                        list.Add(offerID);
                    }
                }
            }
            return list;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // get Offers by OfferID //////////////////////////////////////////////////////////////////////////////
        public List<Offer> GetOfferById(List<int> listOfOfferID)
        {
            List<Offer> listOfOffers = new List<Offer>();
            foreach (int id in listOfOfferID)
            {
                // SQL
                SqlCommand cmd = new SqlCommand("SELECT * FROM Offers WHERE OfferID = @OfferID", conn);
                cmd.Parameters.AddWithValue("@OfferID", id);
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
                            listOfOffers.Add(o);
                        }
                    }
                }                
            }
            return listOfOffers;
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
            // SQL
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Get applications for a job offer ///////////////////////////////////////////////////////////////////////////////
        public List <int> GetApplicationByOfferID(int offerID)
        {
            List<int> list = new List<int>();
            //SQL
            SqlCommand cmd = new SqlCommand("SELECT JobSeekerID FROM Applications WHERE OfferID = @OfferID", conn);
            cmd.Parameters.AddWithValue("@OfferID", offerID);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int jobSeekerID = reader.GetInt32(reader.GetOrdinal("JobSeekerID"));
                        list.Add(jobSeekerID);
                    }
                }
            }
            return list;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Get detail info for each of the Job Seekers to display below the offer in the employer home page ///////////////
        public List <JobSeeker> GetJobSeekerByID(List <int> listOfJobSeekerID)
        {
            List <JobSeeker> listOfJobSeekers = new List<JobSeeker>();
            foreach (int id in listOfJobSeekerID)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM JobSeekers WHERE JSID = @JSID", conn);
                cmd.Parameters.AddWithValue("@JSID", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string firstName = reader.GetString(reader.GetOrdinal("JSFirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("JSLastName"));
                            string email = reader.GetString(reader.GetOrdinal("JSEmail"));
                            string phoneNumber = reader.GetString(reader.GetOrdinal("JSPhone"));
                            string education = reader.GetString(reader.GetOrdinal("JSEducation"));
                            string experience = reader.GetString(reader.GetOrdinal("jsExperience"));
                            JobSeeker js = new JobSeeker() { JSFirstName = firstName, JSLastName = lastName, JSEmail = email, JSPhone = phoneNumber, JSEducation = education, JSsExperience = experience};
                            listOfJobSeekers.Add(js);
                        }
                    }
                }                
            }
            return listOfJobSeekers;
        }

        // OFFERS /////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Delete a job offer /////////////////////////////////////////////////////////////////////////////////////////////
        public void DeleteOfferByID(int id)
        {
            // SQL
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
                cmd.Parameters.AddWithValue("@EmployerID", Globals.CurrentEmployer.EID);
                cmd.ExecuteNonQuery();
                return true;
            }

        }

        // ACCOUNT ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Update the account information /////////////////////////////////////////////////////////////////////////////////
        public bool UpdateEmployerAccountByID(string nameOfCompany, string email, string phoneNumber)
        {
            // SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE Employers SET NameOfCompany = @NameOfCompany, EEmail = @EEmail, EPhone = @EPhone WHERE EID = @EID", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@NameOfCompany", nameOfCompany);
                cmd.Parameters.AddWithValue("@EEmail", email);
                cmd.Parameters.AddWithValue("@EPhone", phoneNumber);
                cmd.Parameters.AddWithValue("@EID", Globals.CurrentEmployer.EID);
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Update Password ////////////////////////////////////////////////////////////////////////////////////////////////
        public bool UpdateEmployerPasswordByID(string password)
        {
            //SQL
            using (SqlCommand cmd = new SqlCommand("UPDATE Employers SET EPassword = @EPassword WHERE EID = @EID", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@EPassword", password);
                cmd.Parameters.AddWithValue("@EID", Globals.CurrentEmployer.EID);
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}


