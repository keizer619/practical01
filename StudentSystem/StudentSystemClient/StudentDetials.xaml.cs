using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentSystemClient.ServiceReference1;

namespace StudentSystemClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference1.Service1Client client;
        DataTable dt;
        DataTable tempDt;

        public MainWindow()
        {
            client = new ServiceReference1.Service1Client();
            InitializeComponent();
            this.populateGrid();
        }

        private void populateGrid()
        {
            try
            {
                dt = new DataTable("Students");
                tempDt = new DataTable("TempStudents");

                tempDt.Columns.Add("StudentId");
                tempDt.Columns.Add("Name");
                tempDt.Columns.Add("DOB");
                tempDt.Columns.Add("GPA");
                tempDt.Columns.Add("Active");

                dt.Columns.Add("StudentId");
                dt.Columns.Add("Name");
                dt.Columns.Add("DOB");
                dt.Columns.Add("GPA");
                dt.Columns.Add("Active");


                StudentC[] stc = client.GetStudents();
                for (int i = 0; i < stc.Length; i++)
                {
                    dt.Rows.Add(stc[i].StudentId, stc[i].Name, stc[i].DOB, stc[i].GPA, stc[i].Active);

                }

                StudentGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("An Error Occured : " + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                NewRegistration dialog = new NewRegistration();
                DataContainer data = new DataContainer();
                data.stc = new StudentC();
                dialog.Data = data;

                dialog.ShowDialog();

                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    tempDt.Rows.Add(data.stc.StudentId, data.stc.Name, data.stc.DOB, data.stc.GPA, data.stc.Active);
                    dt.Rows.Add(data.stc.StudentId, data.stc.Name, data.stc.DOB, data.stc.GPA, data.stc.Active);
                    StudentGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("An Error Occured : " + exc.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.SaveStudents();
            this.populateGrid();
        }

        private void SaveStudents()
        {
            StudentC[] stdc = new StudentC[tempDt.Rows.Count];
            int i = 0;

            try
            {
                foreach (DataRow row in tempDt.Rows)
                {
                    stdc[i] = new StudentC();
                    stdc[i].Name = row[1].ToString();
                    stdc[i].DOB = DateTime.Parse(row[2].ToString());
                    stdc[i].GPA = double.Parse(row[3].ToString());
                    stdc[i].Active = bool.Parse(row[4].ToString());
                    i++;
                }
                client.AddStudent(stdc);
                MessageBox.Show("Data Saved Successully");
            }
            catch (Exception exc)
            {
                MessageBox.Show("An Error Occured : " + exc.Message);
            }
        }

    }
}
