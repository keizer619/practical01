using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessEntities;
using System.Data;
using BussinessLogicTier.Classes;

namespace BussinessLogicTier
{
    public class StudentLogic
    {
        public void AddStudent(BussinessLogicTier.Classes.Student[] sd)
        {
            try
            {
                DataLogic dl = new DataLogic();
                for (int i = 0; i < sd.Length; i++)
                {
                    BussinessEntities.Classes.Student std = new BussinessEntities.Classes.Student();
                    std.Name = sd[i].Name;
                    std.DOB = sd[i].DOB;
                    std.GPA = sd[i].GPA;
                    std.Active = sd[i].Active;
                    dl.AddStudent(std);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            
            
            
        }

        public Student GetStudent(int StudentId)
        {
            Student std = new Student();

            try
            {
                DataLogic dl = new DataLogic();
                BussinessEntities.Classes.Student stdc = dl.GetStudent(StudentId);

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

        public DataTable GetStudent()
        {
            try
            {
                DataLogic dl = new DataLogic();
                return dl.GetStudent();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        
    }
}
