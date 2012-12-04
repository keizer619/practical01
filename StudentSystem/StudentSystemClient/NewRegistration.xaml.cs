using System;
using System.Collections.Generic;
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
using StudentSystemClient.ServiceReference1;

namespace StudentSystemClient
{
    /// <summary>
    /// Interaction logic for NewRegistration.xaml
    /// </summary>
    public partial class NewRegistration : Window
    {
        public DataContainer Data { get; set; }
        

        public NewRegistration()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtGPA.Text != String.Empty && TxtName.Text != String.Empty)
                {
                    StudentC St = new StudentC();
                    //St.StudentId = int.Parse( TxtID.Text);
                    St.Name = TxtName.Text;
                    St.DOB = DateBirthPicker.DisplayDate;
                    St.GPA = double.Parse(TxtGPA.Text);
                    St.Active = CheckActive.IsChecked.Value;

                    Data.stc = St;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Please Fill incomplete fields");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("An Error Occured : " + exc.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        bool AreAllValidNumericChars(string str)
        {
            try
            {
                bool ret = true;
                if (str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator
                    )
                    return ret;

                int l = str.Length;
                for (int i = 0; i < l; i++)
                {
                    char ch = str[i];
                    ret &= Char.IsDigit(ch);
                }

                return ret;
            }
            catch (Exception exc)
            {
                MessageBox.Show("An Error Occured : " + exc.Message);
                return false;
            }
        }

        private void TxtGPA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }
    }
}
