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
    // Database class ///////////////////////////////////////////////////////////////////////////////////////////////////
    class Database
    {
        public int JOBSEEKERID = 0;
        const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=seeker_v1;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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

        // Login for employers //////////////////////////////////////////////////////////////////////////////////////////////       
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
                        Globals.CurrentEmployer.NameOfCompany = reader.GetString(reader.GetOrdinal("nameOfCie"));
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
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Employers (nameOfCie, eEmail, ephone, ePassword) VALUES (@nameOfCie, @eEmail, @ephone, @ePassword)"))
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
            using (SqlCommand cmd = new SqlCommand("INSERT INTO JobSeekers (jsFirstName, jsLastName, jsEmail, jsPassword, jsPhone) VALUES (@jsFirstName, @jsLastName, @jsEmail, @jsPassword, @jsPhone)"))
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
            using (SqlCommand cmd = new SqlCommand("UPDATE JobSeekers SET jsEducation = @jsEducation WHERE jsID = @jsID"))
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}


