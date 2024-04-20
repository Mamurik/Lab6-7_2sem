using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private const string connectionString = "server=localhost;database=librarydb_;user=root;password=alex2005";

        public MainWindow()
        {
            InitializeComponent();
            CreateTables();

            LoadGenres();
            LoadBooks();
            LoadAuthors();


        }

        private void CreateTables()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Genres (
                                                GenreID INT PRIMARY KEY AUTO_INCREMENT,
                                                GenreName VARCHAR(255)
                                            )";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Authors (
                                                AuthorID INT PRIMARY KEY AUTO_INCREMENT,
                                                FirstName VARCHAR(255),
                                                LastName VARCHAR(255),
                                                BirthDate DATE
                                            )";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Books (
                                                BookID INT PRIMARY KEY AUTO_INCREMENT,
                                                Title VARCHAR(255),
                                                YearPublished INT,
                                                PageCount INT,
                                                GenreName VARCHAR(255),
                                                LastName VARCHAR(255),
                                                LibraryStock INT
                                                
                                            )";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Таблицы успешно созданы.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при создании таблиц: " + ex.Message);
                }
            }
        }

        private void LoadData<T>(string tableName, DataGrid dataGrid)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {tableName}", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void LoadGenres()
        {
            LoadData<Genre>("Genres", genresDataGrid);
        }

        private void LoadBooks()
        {
            LoadData<Book>("Books", booksDataGrid);
        }

        private void LoadAuthors()
        {
            LoadData<Author>("Authors", authorsDataGrid);
        }
    

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            LoadGenres();
            LoadBooks();
            LoadAuthors();
        }
        private void SaveChanges()
        {
            DataTable genresDataTable = ((DataView)genresDataGrid.ItemsSource).Table;
            DataTable booksDataTable = ((DataView)booksDataGrid.ItemsSource).Table;
            DataTable authorsDataTable = ((DataView)authorsDataGrid.ItemsSource).Table;

            if (!DataValidator<Genre>.ValidateData(genresDataTable) ||
                !DataValidator<Book>.ValidateData(booksDataTable) ||
                !DataValidator<Author>.ValidateData(authorsDataTable))
            {
                return; 
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter genresAdapter = new MySqlDataAdapter("SELECT * FROM Genres", connection);
                    MySqlCommandBuilder genresCmdBuilder = new MySqlCommandBuilder(genresAdapter);
                    genresAdapter.Update(genresDataTable);

                    MySqlDataAdapter booksAdapter = new MySqlDataAdapter("SELECT * FROM Books", connection);
                    MySqlCommandBuilder booksCmdBuilder = new MySqlCommandBuilder(booksAdapter);
                    booksAdapter.Update(booksDataTable);

                    MySqlDataAdapter authorsAdapter = new MySqlDataAdapter("SELECT * FROM Authors", connection);
                    MySqlCommandBuilder authorsCmdBuilder = new MySqlCommandBuilder(authorsAdapter);
                    authorsAdapter.Update(authorsDataTable);

                    MessageBox.Show("изменения сохранены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ошибка сохранения: " + ex.Message);
                }
            }
        }
    }    
}