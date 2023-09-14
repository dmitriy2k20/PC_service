using Microsoft.Win32;
using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace PC_service
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        DbPHPAdmin dt = new DbPHPAdmin();
        private const ResizeMode noResize = ResizeMode.CanMinimize;
        private MySqlConnection connection;
        private MySqlCommand command;
        public AdminPanel()
        {
            InitializeComponent();
            ResizeMode = noResize;
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Hide();
                Autoris auth = new Autoris();
                auth.Show();
            }
        }

        private void CbSelectPerformance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbSelectPerformance.SelectedIndex == 0)
            {
                DgOrders.Visibility = Visibility.Hidden;
                DgClients.Visibility = Visibility.Visible;

                string connectionString = "server=127.0.0.1;user=root;password=root;database=PcService; port =3333";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                // Retrieve data from MySQL database and populate DataGrid
                List<DatabaseClass> dataList = new List<DatabaseClass>();
                command = new MySqlCommand("SELECT * FROM Clients", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseClass dt = new DatabaseClass()
                    {
                        id = reader.GetInt32(0),
                        FIO = reader.GetString(1),
                        Phone = reader.GetString(2)
                    };
                    dataList.Add(dt);
                }
                reader.Close();
                DgClients.ItemsSource = dataList;
            }

            if (CbSelectPerformance.SelectedIndex == 1)
            {
               DgClients.Visibility = Visibility.Hidden;
               DgOrders.Visibility = Visibility.Visible;

                string connectionString = "server=127.0.0.1;user=root;password=root;database=PcService; port =3333";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                // Retrieve data from MySQL database and populate DataGrid
                List<DatabaseClass> dataList = new List<DatabaseClass>();
                command = new MySqlCommand("SELECT * FROM Orders", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseClass dt = new DatabaseClass()
                    {
                        id = reader.GetInt32(0),
                        FIO = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Description = reader.GetString(3)
                    };
                    dataList.Add(dt);
                }
                reader.Close();
                DgOrders.ItemsSource = dataList;
            }
        }

        private void BtDel_Click(object sender, RoutedEventArgs e)
        {
            if (CbSelectPerformance.SelectedIndex == 0)
            {
                var selectedItem = DgClients.SelectedItem;

                if (selectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите запись для удаления");
                    return;
                }

                if (selectedItem != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        string connectionString = "server=127.0.0.1;user=root;password=root;database=PcService; port=3333";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Clients WHERE id=@idClient";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@idClient", (int)selectedItem.GetType().GetProperty("id").GetValue(selectedItem));
                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Запись удалена!");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка!", "Внимание!");
                            }

                            this.Hide();
                            AdminPanel del = new AdminPanel();
                            del.Show();
                        }
                    }
                }
            }


            if (CbSelectPerformance.SelectedIndex == 1)
            {
                var selectedItem = DgOrders.SelectedItem;

                if (selectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите запись для удаления");
                    return;
                }

                if (selectedItem != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        string connectionString = "server=127.0.0.1;user=root;password=root;database=PcService; port=3333";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Orders WHERE id=@idOrders";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@idOrders", (int)selectedItem.GetType().GetProperty("id").GetValue(selectedItem));
                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Запись удалена!");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка!", "Внимание!");
                            }

                            this.Hide();
                            AdminPanel del = new AdminPanel();
                            del.Show();
                        }
                    }
                }
            }
        }

        private void ChangedMondey_Click(object sender, RoutedEventArgs e)
        {
            if (CbSelectPerformance.SelectedIndex == 0)
            {
                int SelectedIndex = DgClients.SelectedIndex;
                if (SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите ячейку для редактирования");
                    return;
                }
                foreach (DatabaseClass dt in DgClients.ItemsSource)
                {
                    command = new MySqlCommand("UPDATE Clients SET FIO=@FIO, Phone=@Phone WHERE id=@id", connection);
                    command.Parameters.AddWithValue("@FIO", dt.FIO);
                    command.Parameters.AddWithValue("@Phone", dt.Phone);
                    command.Parameters.AddWithValue("@id", dt.id);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Данные изменены");
                this.Hide();
                AdminPanel update = new AdminPanel();
                update.Show();
            }

            if (CbSelectPerformance.SelectedIndex == 1)
            {
                int SelectedIndex = DgOrders.SelectedIndex;
                if (SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите ячейку для редактирования");
                    return;
                }
                foreach (DatabaseClass dt in DgOrders.ItemsSource)
                {
                    command = new MySqlCommand("UPDATE Orders SET FIO=@FIO, Phone=@Phone, Description=@Description WHERE id=@id", connection);
                    command.Parameters.AddWithValue("@FIO", dt.FIO);
                    command.Parameters.AddWithValue("@Phone", dt.Phone);
                    command.Parameters.AddWithValue("@Description", dt.Description);
                    command.Parameters.AddWithValue("@id", dt.id);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Данные изменены");
                this.Hide();
                AdminPanel update = new AdminPanel();
                update.Show();
            }
        }

        private void BtInsert_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (CbSelectPerformance.SelectedIndex == 0)
            {
                int SelectedIndex = DgClients.SelectedIndex;
                if (SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите ячейку для добавления");
                    return;
                }
                foreach (DatabaseClass dt in DgClients.ItemsSource)
                {
                    command = new MySqlCommand("SELECT COUNT(*) FROM Clients WHERE id=@id", connection);
                    command.Parameters.AddWithValue("@id", dt.id);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        command = new MySqlCommand("UPDATE Clients SET FIO=@FIO, Phone=@Phone WHERE id=@id", connection);
                    }
                    else
                    {
                        command = new MySqlCommand("INSERT INTO Clients (FIO, Phone) VALUES (@FIO, @Phone)", connection);
                    }
                    command.Parameters.AddWithValue("@FIO", dt.FIO);
                    command.Parameters.AddWithValue("@Phone", dt.Phone);
                    command.Parameters.AddWithValue("@id", dt.id);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Данные успешно добавлены");
                this.Hide();
                AdminPanel main = new AdminPanel();
                main.Show();
            }


            if (CbSelectPerformance.SelectedIndex == 1)
            {
                int SelectedIndex = DgOrders.SelectedIndex;
                if (SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите ячейку для добавления");
                    return;
                }
                foreach (DatabaseClass dt in DgOrders.ItemsSource)
                {
                    command = new MySqlCommand("SELECT COUNT(*) FROM Orders WHERE id=@id", connection);
                    command.Parameters.AddWithValue("@id", dt.id);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        command = new MySqlCommand("UPDATE Orders SET FIO=@FIO, Phone=@Phone, Description=@Description WHERE id=@id", connection);
                    }
                    else
                    {
                        command = new MySqlCommand("INSERT INTO Orders (FIO, Phone, Description) VALUES (@FIO, @Phone, @Description)", connection);
                    }
                    command.Parameters.AddWithValue("@FIO", dt.FIO);
                    command.Parameters.AddWithValue("@Phone", dt.Phone);
                    command.Parameters.AddWithValue("@Description", dt.Description);
                    command.Parameters.AddWithValue("@id", dt.id);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Данные успешно добавлены");
                this.Hide();
                AdminPanel main = new AdminPanel();
                main.Show();
            }



        }

        private void DgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
