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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text.Trim();
            string phone = tbPhone.Text.Trim();
            string pass = pbPassword.Password.Trim();

            if (name == "" || pass == "")
            {
                MessageBox.Show("Заполни все поля");
                return;
            }

            using (var db = new PasSchoolEntities1())
            {
                bool exists = db.Student.Any(x => x.Phone == phone);

                if (exists)
                {
                    MessageBox.Show("Этот телефон уже зарегистрирован");
                    return;
                }

                var student = new Student
                {
                    FullName = name,
                    Phone = phone,
                    Password = pass
                };

                db.Student.Add(student);
                db.SaveChanges();

                MessageBox.Show("Регистрация успешна");
                Close();
            }
        }
    }
}