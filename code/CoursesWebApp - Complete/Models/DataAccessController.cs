using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesWebApp.Models
{
    public class DataAccessController
    {
        private string connectionString = "<Azure SQL Database Connectoion String>";

        // Retrieve all details of courses and their modules    
        public IEnumerable<CoursesAndModules> GetAllCoursesAndModules()
        {
            List<CoursesAndModules> courseList = new List<CoursesAndModules>();

            // Connect to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Specify the SQL query to run
                SqlCommand cmd = new SqlCommand(
                    @"SELECT c.CourseName, m.ModuleTitle, s.ModuleSequence
                      FROM dbo.Courses c JOIN dbo.StudyPlans s
                      ON c.CourseID = s.CourseID
                      JOIN dbo.Modules m
                      ON m.ModuleCode = s.ModuleCode
                      ORDER BY c.CourseName, s.ModuleSequence", con);
                cmd.CommandType = CommandType.Text;

                // Execute the query
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                // Read the data a row at a time
                while (rdr.Read())
                {
                    string courseName = rdr["CourseName"].ToString();
                    string moduleTitle = rdr["ModuleTitle"].ToString();
                    int moduleSequence = Convert.ToInt32(rdr["ModuleSequence"]);
                    CoursesAndModules course = new CoursesAndModules(courseName, moduleTitle, moduleSequence);
                    courseList.Add(course);
                }

                // Close the database connection
                con.Close();
            }
            return courseList;
        }
    }
}
