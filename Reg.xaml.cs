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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        DbPHPAdmin dt = new DbPHPAdmin();
        private const ResizeMode noResize = ResizeMode.CanMinimize;
        private MySqlConnection connection;
        private MySqlCommand command;
        public Reg()
        {
            InitializeComponent();
            ResizeMode = noResize;
        }

        private void BtRegClient_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text == "" || PbPassword.Password == "")
            {
                MessageBox.Show("В полях для добавления данных ничего нет");
                return;
            }

            String NoteText0 = TbLogin.Text;
            String NoteText1 = PbPassword.Password;

            DbPHPAdmin db = new DbPHPAdmin();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `Login`, `Password`) VALUES (NULL, @Login, @Password)", db.getConnection());
            command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = NoteText0;
            command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = NoteText1;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Вы успешно зарегистрированы");

            else
            {
                MessageBox.Show("Ошибка!");
            }

            db.closeConnection();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
                this.Hide();
                Autoris auth = new Autoris();
                auth.Show();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbLogin.Focus();
        }
    }
}
