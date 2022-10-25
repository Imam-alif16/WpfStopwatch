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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfStopwatch.MVVM.ViewModel;
using System.Data.SqlClient;
using System.Data;

namespace WpfStopwatch.MVVM.View
{
    /// <summary>
    /// Interaction logic for Page1View.xaml
    /// </summary>
    public partial class Page1View : UserControl
    {
        public Page1View()
        {
            InitializeComponent();

            DataContext = new Page1ViewModel();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-9DNQMPJG\SQLEXPRESS;Initial Catalog=NewDB;Integrated Security=True");

        public void clearData()
        {
            process_txt.Clear();
            start_txt.Clear();
            finish_txt.Clear();
        }

        public bool isValid()
        {
            if (process_txt.Text == string.Empty)
            {
                MessageBox.Show("Process is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (start_txt.Text == string.Empty)
            {
                MessageBox.Show("Start URL is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (finish_txt.Text == string.Empty)
            {
                MessageBox.Show("Finish URL is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO FirstTable VALUES (@Process, @Start, @Finish)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Process", process_txt.Text);
                    cmd.Parameters.AddWithValue("@Start", start_txt.Text);
                    cmd.Parameters.AddWithValue("@Finish", finish_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
