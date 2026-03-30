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
    public partial class TeacherStubWindow : Window
    {
        private int _teacherId;
        private string _fullName;

        public TeacherStubWindow(int teacherId, string fullName)
        {
            InitializeComponent();
            _teacherId = teacherId;
            _fullName = fullName;

            tbHello.Text = "Привет, " + _fullName + "!";
        }

        private void BtnMyCourses_Click(object sender, RoutedEventArgs e)
        {
            new TeacherCoursesWindow(_teacherId).ShowDialog();
        }

        private void BtnGrades_Click(object sender, RoutedEventArgs e)
        {
            new TeacherGradesWindow(_teacherId).ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}