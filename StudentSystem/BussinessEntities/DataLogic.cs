using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BussinessEntities.Classes;

namespace BussinessEntities
{
    public class DataLogic
    {
        string connString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=StudentDB; User ID=sa;Password=cenmetrix@123;";

        public void AddStudent(Student std)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                using (var command = new SqlCommand("AddStudent", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
              
                })
                {
                    command.Parameters.Add(new SqlParameter("@StudentName",std.Name));
                    command.Parameters.Add(new SqlParameter("@DOB", std.DOB));
                    command.Parameters.Add(new SqlParameter("@GPA", std.GPA));
                    command.Parameters.Add(new SqlParameter("@Active", std.Active));

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Student GetStudent(int StudentId)
        {
            Student st = new Student();
            st.StudentId = StudentId;

            try
            {
                using (var conn = new SqlConnection(connString))
                using (var command = new SqlCommand("GetStudent", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure

                })
                {
                    command.Parameters.Add(new SqlParameter("@StudentId", StudentId));
                    conn.Open();
                    SqlDataReader dr = command.ExecuteReader();
                   
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            st.Name = dr.GetString(0);
                        }
                        if (!dr.IsDBNull(1))
                        {
                            st.DOB = dr.GetDateTime(1);
                        } 
                        if (!dr.IsDBNull(2))
                        {
                            st.GPA = (double)dr.GetDecimal(2);
                        }
                        if (!dr.IsDBNull(3))
                        {
                            st.Active = dr.GetBoolean(3);
                        }
                    }
                    dr.Close();
                    conn.Close();
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }

            return st;
        }

        public DataTable GetStudent()
        {
            DataTable dt = new DataTable("Students");
            dt.Columns.Add("StudentId");
            dt.Columns.Add("Name");
            dt.Columns.Add("DOB");
            dt.Columns.Add("GPA");
            dt.Columns.Add("Active");

            try
            {
                using (var conn = new SqlConnection(connString))
                using (var command = new SqlCommand("GetAllStudents", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure

                })
                {
                    
                    conn.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        dt.Rows.Add(dr["StudentId"], dr["Name"], dr["DOB"], dr["GPA"], dr["Active"]);
                    }
                    dr.Close();
                    conn.Close();
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }

            return dt;
        }


        //public Student GetAllStudents()
        //{
        //    Student []st = new Student();

        //    try
        //    {
        //        using (var conn = new SqlConnection(connString))
        //        using (var command = new SqlCommand("GetStudent", conn)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure

        //        })
        //        {
        //            command.Parameters.Add(new SqlParameter("@StudentId", StudentId));
        //            conn.Open();
        //            SqlDataReader dr = command.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                //st.StudentId = dr.GetInt32(0);
        //                st.Name = dr.GetString(0);
        //                st.DOB = dr.GetDateTime(1);
        //                st.GPA = dr.GetDouble(2);
        //                st.Active = dr.GetBoolean(3);
        //            }

        //            conn.Close();
        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }

        //    return st;
        //}
    }
}
