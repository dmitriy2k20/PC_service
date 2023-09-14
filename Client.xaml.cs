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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        DbPHPAdmin dt = new DbPHPAdmin();
        private const ResizeMode noResize = ResizeMode.CanMinimize;
        private MySqlConnection connection;
        private MySqlCommand command;
        public Client()
        {
            InitializeComponent();
            ResizeMode = noResize;
        }

        private void BtInsert_Click(object sender, RoutedEventArgs e)
        {
            if (TbFio.Text == "" || TbPhone.Text == "" || TbDescription.Text == "")
            {
                MessageBox.Show("В полях для добавления данных ничего нет");
                return;
            }

            String NoteText0 = TbFio.Text;
            String NoteText1 = TbPhone.Text;
            String NoteText2 = TbDescription.Text;

            DbPHPAdmin db = new DbPHPAdmin();
            MySqlCommand command = new MySqlCommand("INSERT INTO `Orders` (`id`, `FIO`, `Phone` , `Description`) VALUES (NULL, @FIO, @Phone , @Description)", db.getConnection());
            command.Parameters.Add("@FIO", MySqlDbType.VarChar).Value = NoteText0;
            command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = NoteText1;
            command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = NoteText2;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Запись сохранена");

            else
            {
                MessageBox.Show("Запись не сохранилась");
            }

            db.closeConnection();

            this.Hide();
            Client main = new Client();
            main.Show();
        }

        private void TbFio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbFio.Text))
            {
                BtInsert.IsEnabled = false;
            }
            else
            {
                BtInsert.IsEnabled = true;
            }

            if (string.IsNullOrEmpty(TbFio.Text))
            {
                BtClear.IsEnabled = false;
            }
            else
            {
                BtClear.IsEnabled = true;
            }

           
        }

        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            if (TbFio.Text != "" || TbPhone.Text != "" || TbDescription.Text != "")
            {
                if (MessageBox.Show("Вы действительно хотите очистить все внесенные данные?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    TbFio.Text = "";
                    TbPhone.Text = "";
                    TbDescription.Text = "";
                }
                else
                {
                }
            }
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
    }
}
