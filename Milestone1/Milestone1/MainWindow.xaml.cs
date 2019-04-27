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
using Npgsql;

namespace Milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<string> selectedCategories = new List<string>();

        public class Business
        {
            public string name { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public double stars { get; set; }
            public int num_reviews { get; set; }
            public double avg_review { get; set; }
            public int checkins { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            addStates();
            addColums2Grid();
            addSortPrefrences();
        }
        private string buildConnString()
        {
            return "Host=localhost; Username=postgres; Password=password; Database=milestone2";
        }

        public void addStates()
        {
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT distinct b_state FROM business ORDER BY b_state;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stateList.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        public void addCities()
        {
            cityList.Items.Clear();
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;

                    cmd.CommandText = "SELECT DISTINCT city FROM business WHERE b_state = '" + stateList.SelectedItem.ToString() +
                        "' ORDER BY city;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cityList.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        public void addZipcodes()
        {
            zipcodeList.Items.Clear();

            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;

                    cmd.CommandText = "SELECT DISTINCT zipcode FROM business WHERE b_state = '" + stateList.SelectedItem.ToString() + 
                        "' AND city ='" + cityList.SelectedItem.ToString() + "' ORDER BY zipcode;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zipcodeList.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        public void addCategories()
        {
            categoryList.Items.Clear();
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;

                    cmd.CommandText = "SELECT DISTINCT c.category_name FROM categories as c, business as b WHERE b.b_state = '" + 
                        stateList.SelectedItem.ToString() + "' AND b.city ='" + cityList.SelectedItem.ToString() + 
                        "' AND b.zipcode = '" + zipcodeList.SelectedItem.ToString() + "' AND b.business_id = c.business_id ORDER BY category_name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryList.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        public void addColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Business Name";
            col1.Binding = new Binding("name");
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Address";
            col2.Binding = new Binding("address");
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "City";
            col3.Binding = new Binding("city");
            businessGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "State";
            col4.Binding = new Binding("state");
            businessGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Stars";
            col5.Binding = new Binding("stars");
            businessGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "# of Reviews";
            col6.Binding = new Binding("num_reviews");
            businessGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Avg Review Rating";
            col7.Binding = new Binding("avg_review");
            businessGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Header = "Total Checkins";
            col8.Binding = new Binding("checkins");
            businessGrid.Columns.Add(col8);
        }

        public void addSortPrefrences()
        {
            sortList.Items.Add("Name (default)");
            sortList.Items.Add("Ratings");
            sortList.Items.Add("Most Reviews");
            sortList.Items.Add("Highest Reviews");
            sortList.Items.Add("Check-Ins");
            sortList.Items.Add("Nearest");
            sortList.SelectedIndex = 0;
        }

        private void StateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessGrid.Items.Clear();
            cityList.Items.Clear();
            zipcodeList.Items.Clear();
            categoryList.Items.Clear();

            if (stateList.SelectedIndex > -1)
            {
                addCities();
            }
        }

        private void cityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessGrid.Items.Clear();
            zipcodeList.Items.Clear();
            categoryList.Items.Clear();

            if (cityList.SelectedIndex > -1)
            {
                addZipcodes();  
            }

        }

        private void zipcodeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryList.Items.Clear();

            if (zipcodeList.SelectedIndex > -1)
            {
                addCategories();
            }

        }

        private string getSortPrefrence()
        {
            string order = " ORDER BY ";
            switch (sortList.SelectedItem.ToString())
            {
                case "Name (default)":
                    order += "b.name;";
                    break;
                case "Ratings":
                    break;
                case "Most Reviews":
                    break;
                case "Highest Reviews":
                    break;
                case "Check-Ins":
                    break;
                case "Nearest":
                    break;
                default:
                    order += "b.name;";
                    break;
            }
            return order;
        }

        private void BusinessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> temp = new List<string>();
            foreach(string category in categoryList.SelectedItems)
            {
                temp.Add(category);
                selectedCategories.Add(category);
            }
            foreach(string category in temp)
            {
                selectedCategoriesList.Items.Add(category);
                categoryList.Items.Remove(category);
            }
        }

        private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> temp = new List<string>();
            foreach(string category in selectedCategoriesList.SelectedItems)
            {
                temp.Add(category);
                selectedCategories.Remove(category);
            }
            foreach(string category in temp)
            {
                selectedCategoriesList.Items.Remove(category);
                categoryList.Items.Add(category);
            }
        }

        private void SearchBusinessesButton_Click(object sender, RoutedEventArgs e)
        {
            businessGrid.Items.Clear();

            if (zipcodeList.SelectedIndex > -1)
            {
                using (var comm = new NpgsqlConnection(buildConnString()))
                {
                    comm.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = comm;
                        cmd.CommandText = "SELECT DISTINCT b.name, b.address, b.city, b.b_state, b.stars, b.review_count, b.review_rating, b.num_checkins " +
                            "FROM business as b, categories as c WHERE b.b_state = '" +
                            stateList.SelectedItem.ToString() + "' AND b.city = '" + cityList.SelectedItem.ToString() +
                            "' AND b.zipcode = '" + zipcodeList.SelectedItem.ToString() + "' AND b.business_id = c.business_id";
                        for(int i = 0; i < selectedCategories.Count(); i++)
                        {
                            if(i == 0)
                            {
                                cmd.CommandText += " AND (";
                            }
                            cmd.CommandText += "c.category_name = '" + selectedCategories[i] + "'";
                            if(i != selectedCategories.Count() - 1)
                            {
                                cmd.CommandText += " OR ";
                            } else
                            {
                                cmd.CommandText += ")";
                            }
                        }
                        cmd.CommandText += getSortPrefrence();
                        using (var reader = cmd.ExecuteReader())
                        {
                            int count = 0;
                            while (reader.Read())
                            {
                                businessGrid.Items.Add(new Business() { name = reader.GetString(0), address = reader.GetString(1), city = reader.GetString(2), state = reader.GetString(3),
                                 stars = reader.GetDouble(4), num_reviews = reader.GetInt32(5),avg_review = reader.GetDouble(6), checkins = reader.GetInt32(7) });
                                count++;
                            }
                            numBusinessesLabel.Content = count.ToString();
                        }
                    }
                    comm.Close();
                }
            }
        }
    }
}