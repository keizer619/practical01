using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BussinessLogicTier;
using BussinessLogicTier.Classes;

namespace PresentationServiceTier
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public void AddStudent(StudentC []stdc)
        {
            try
            {
                StudentLogic sl = new StudentLogic();

                Student[] std = new Student[stdc.Length];

                for (int i = 0; i < stdc.Length; i++)
                {
                    std[i] = new Student();
                    std[i].Name = stdc[i].Name;
                    std[i].DOB = stdc[i].DOB;
                    std[i].GPA = stdc[i].GPA;
                    std[i].Active = stdc[i].Active;
                }
                sl.AddStudent(std);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        public StudentC GetStudent(int StudentId)
        {
            StudentC std = new StudentC();

            try
            {
                StudentLogic sl = new StudentLogic();
                Student stdc = sl.GetStudent(StudentId);

                std.StudentId = stdc.StudentId;
                std.Name = stdc.Name;
                std.DOB = stdc.DOB;
                std.GPA = stdc.GPA;
                std.Active = stdc.Active;
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return std;
        }

        public StudentC [] GetStudents()
        {

            try
            {
                StudentLogic sl = new StudentLogic();
                DataTable dt = sl.GetStudent();
                StudentC [] std = new StudentC[dt.Rows.Count];


                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    std[i] = new StudentC();
                    std[i].StudentId = Int32.Parse( row[0].ToString());
                    std[i].Name = row[1].ToString();
                    std[i].DOB = DateTime.Parse( row[2].ToString());
                    std[i].GPA = (double)Decimal.Parse(row[3].ToString());
                    std[i].Active = bool.Parse( row[4].ToString());
                    i++;

                }
                return std;
            }
            catch (Exception exc)
            {
                throw exc;
            }

          
        }


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
