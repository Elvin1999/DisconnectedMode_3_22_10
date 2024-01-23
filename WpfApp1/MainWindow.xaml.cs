using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        string cs = "";
        DataTable table;
        SqlDataReader reader;
        SqlDataAdapter da;
        public MainWindow()
        {
            InitializeComponent();
            //using (conn = new SqlConnection())
            //{
            //    cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";
            //    conn.ConnectionString = cs;
            //    conn.Open();

            //    var set = new DataSet();
            //    SqlCommand command = new SqlCommand("SELECT * FROM Authors",conn);

            //    var da = new SqlDataAdapter();
            //    da.SelectCommand=command;
            //    da.Fill(set, "AuthorSet");

            //    myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
            //}
        }

        private void showBtn_Click(object sender, RoutedEventArgs e)
        {
            #region DataTable
            //using (var conn=new SqlConnection())
            //{
            //    cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";
            //    conn.ConnectionString = cs;
            //    conn.Open();

            //    SqlCommand command = new SqlCommand();
            //    command.CommandText = "SELECT * FROM Authors";
            //    command.Connection = conn;

            //    table = new DataTable();

            //    bool hasColumnAdded = false;

            //    using (var reader=command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            if (!hasColumnAdded)
            //            {
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    table.Columns.Add(reader.GetName(i));
            //                }
            //                hasColumnAdded = true;
            //            }

            //            DataRow row=table.NewRow();
            //            for (int i = 0; i < reader.FieldCount; i++)
            //            {
            //                row[i] = reader[i];
            //            }
            //            table.Rows.Add(row);

            //        }

            //        myDataGrid1.ItemsSource = table.DefaultView;
            //    }
            //}
            #endregion

            #region DataSet and SqlDataAdapter

            using (var conn = new SqlConnection())
            {
                cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";
                conn.ConnectionString = cs;
                conn.Open();

                set = new DataSet();
                da = new SqlDataAdapter("SELECT * FROM Authors;SELECT * FROM Books", conn);
                da.Fill(set, "authorsBooks");

                myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
                myDataGrid2.ItemsSource = set.Tables[1].DefaultView;
            }

            #endregion
        }

        DataSet set = new DataSet();


        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";
                conn.ConnectionString = cs;
                conn.Open();

                var command = new SqlCommand("UPDATE Authors SET FirstName=@firstName WHERE Id=@id", conn);

                command.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@id",
                    Value = 1
                });

                command.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@firstName",
                    Value = "TEST TEST"
                });

                da = new SqlDataAdapter();
                da.UpdateCommand = command;
                da.UpdateCommand.ExecuteNonQuery();

                da.Update(set, "authorsBooks");


                myDataGrid1.ItemsSource = set.Tables[0].DefaultView;





            }
        }
    }
}
