using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace PC_service
{
    /// <summary>
    /// Логика взаимодействия для Autoris.xaml
    /// </summary>
    public partial class Autoris : Window
    {
        DatabaseClass dt = new DatabaseClass();
        private const ResizeMode noResize = ResizeMode.CanMinimize;
        public Autoris()
        {
            InitializeComponent();
            ResizeMode = noResize;
        }

        private void BtEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" || PbPassword.Password != "")
            {
                if (TbLogin.Text == "Admin" || TbLogin.Text == "admin")
                {
                    string connectionString = "server=127.0.0.1;user=root;password=root;database=PcService; port =3333";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(*) FROM admin WHERE Login = @login AND Password = @password";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@login", TbLogin.Text);
                        command.Parameters.AddWithValue("@password", PbPassword.Password);
                        int result = Convert.ToInt32(command.ExecuteScalar());
                        if (result > 0)
                        {
                            MessageBox.Show("Авторизация прошла успешно");
                            this.Hide();
                            AdminPanel main = new AdminPanel();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль");
                            return;
                        }
                    }
                }

                if (TbLogin.Text != "admin" || TbLogin.Text != "admin")
                {
                    string connectionString = "server=127.0.0.1;user=root;password=root;database=PcService; port =3333";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                                                                                                    connection.Open();
                        string query = "SELECT COUNT(*) FROM users WHERE Login = @login AND Password = @password";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@login", TbLogin.Text);
                        command.Parameters.AddWithValue("@password", PbPassword.Password);
                        int result = Convert.ToInt32(command.ExecuteScalar());
                        if (result > 0)
                        {
                            MessageBox.Show("Авторизация прошла успешно");
                            this.Hide();
                            Client main = new Client();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Неверное имя пользователя или пароль!");
                        }
                    }
                }

                if (TbLogin.Text == "" || PbPassword.Password == "")
                {
                 MessageBox.Show("Введите логин или пароль", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
             }

            }



        }

        private void BtRegClient_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Reg reg = new Reg();
            reg.Show();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            TbLogin.Focus();
        }
    }
}
