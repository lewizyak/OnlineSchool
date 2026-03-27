using OnlineSchool.Model;
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
using System.Windows.Shapes;

namespace OnlineSchool.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            cbRole.Items.Add("Ученик");
            cbRole.Items.Add("Преподаватель");
            cbRole.Items.Add("Администратор");
            cbRole.SelectedIndex = 0;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string role = cbRole.SelectedItem?.ToString();
            string phone = tbPhone.Text.Trim();
            string pass = pbPassword.Password.Trim();

            if (role == "Преподаватель")
            {
                new TeacherStubWindow().Show();
                Close();
                return;
            }

            if (role == "Администратор")
            {
                new AdminStubWindow().Show();
                Close();
                return;
            }

            if (phone == "" || pass == "")
            {
                MessageBox.Show("Заполни телефон и пароль");
                return;
            }

            using (var db = new PasSchoolEntities())
            {
                var student = db.Student
                    .FirstOrDefault(s => s.Phone == phone && s.Password == pass);

                if (student == null)
                {
                    MessageBox.Show("Неверный телефон или пароль");
                    return;
                }

                new StudentMainWindow(student.StudentId, student.FullName).Show();
                Close();
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().ShowDialog();
        }
    }
}
