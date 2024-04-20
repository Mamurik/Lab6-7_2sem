using System;
using System.Data;
using System.Windows;

namespace WpfApp2
{
    public static class DataValidator<T>
    {
        public static bool ValidateData(DataTable dataTable)
        {
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (!ValidateRow(row))
                            return false;
                    }
                }
            }

            return true;
        }

        private static bool ValidateRow(DataRow row)
        {
            if (typeof(T) == typeof(Genre))
                return ValidateGenresRow(row);
            else if (typeof(T) == typeof(Book))
                return ValidateBooksRow(row);
            else if (typeof(T) == typeof(Author))
                return ValidateAuthorsRow(row);

            return true;
        }

        private static bool ValidateGenresRow(DataRow row)
        {
            if (string.IsNullOrEmpty(row["GenreName"] as string))
            {
                MessageBox.Show("Введите название жанра.");
                return false;
            }

            string genreName = row["GenreName"].ToString();

            // Проверка на допустимые значения жанров
            if (genreName != "Роман" && genreName != "Детектив" && genreName != "Научная" && genreName != "Фантастика")
            {
                MessageBox.Show("Неверное значение жанра.");
                return false;
            }

            return true;
        }

        private static bool ValidateBooksRow(DataRow row)
        {
            if (string.IsNullOrEmpty(row["Title"] as string))
            {
                MessageBox.Show("Введите название книги.");
                return false;
            }

            if (!int.TryParse(row["YearPublished"].ToString(), out _))
            {
                MessageBox.Show("Неверный формат года публикации.");
                return false;
            }

            if (!int.TryParse(row["PageCount"].ToString(), out _))
            {
                MessageBox.Show("Неверный формат количества страниц.");
                return false;
            }

            if (row["GenreName"] == DBNull.Value)
            {
                MessageBox.Show("Выберите жанр.");
                return false;
            }

            Genre genre;
            if (!Enum.TryParse(row["GenreName"].ToString(), out genre))
            {
                MessageBox.Show("Неверное значение жанра.");
                return false;
            }

            if (!int.TryParse(row["LibraryStock"].ToString(), out _))
            {
                MessageBox.Show("Неверный формат количества книг в библиотеке.");
                return false;
            }

            return true;
        }

        private static bool ValidateAuthorsRow(DataRow row)
        {
            if (string.IsNullOrEmpty(row["FirstName"] as string))
            {
                MessageBox.Show("Введите имя автора.");
                return false;
            }

            if (string.IsNullOrEmpty(row["LastName"] as string))
            {
                MessageBox.Show("Введите фамилию автора.");
                return false;
            }

            if (!DateTime.TryParse(row["BirthDate"].ToString(), out _))
            {
                MessageBox.Show("Неверный формат даты рождения.");
                return false;
            }

            return true;
        }
    }
}