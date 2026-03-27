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
    /// Логика взаимодействия для StudentMainWindow.xaml
    /// </summary>
    public partial class StudentMainWindow : Window
    {
        private int _studentId;
        private string _fullName;

        public StudentMainWindow(int studentId, string fullName)
        {
            InitializeComponent();

            _studentId = studentId;
            _fullName = fullName;

            tbHello.Text = "Привет, " + _fullName + "!";
        }

        private void BtnMyCourses_Click(object sender, RoutedEventArgs e)
        {
            new MyCourses(_studentId).ShowDialog();
        }

        private void BtnAvailableCourses_Click(object sender, RoutedEventArgs e)
        {
            new AvailableCourses(_studentId).ShowDialog();
        }

        private void BtnGrades_Click(object sender, RoutedEventArgs e)
        {
            new GradesWindow(_studentId).ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}