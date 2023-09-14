using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PC_service
{
    class DatabaseClass
    {
        public string Title { get; set; }
        public string Cost { get; set; }
        public string Description { get; set; }
        public string MainImagePath { get; set; }
        public string IsActive { get; set; }
        public string Manufacturer { get; set; }

        public string Pasport { get; set; }
        public string Division_code { get; set; }
        public string Payment_ratio { get; set; }
        public string Category { get; set; }


        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO { get; set; }
        public string Birthday { get; set; }
        public string RegistrationDate { get; set; }
        public string RegistrationTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int id { get; set; }


        MySqlConnectionStringBuilder connectionstring;
        string StrConnection = @"server=127.0.0.1;database=Project;user id=root;password=root; port = 3333;";

        public DatabaseClass()
        {
            connectionstring = new MySqlConnectionStringBuilder();

            connectionstring.Server = "127.0.0.1";
            connectionstring.UserID = "root";
            connectionstring.Port = 3333;
            connectionstring.Password = "root";
            connectionstring.Database = "Project";
        }
    }
}
 5ё  