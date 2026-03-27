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
    /// Логика взаимодействия для MyCourses.xaml
    /// </summary>
    public partial class MyCourses : Window
    {
        private int _studentId;

        public MyCourses(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;

            LoadCourses();
        }

        private void LoadCourses()
        {
            using (var db = new PasSchoolEntities())
            {
                var list = db.StudentCourse
                    .Where(sc => sc.StudentId == _studentId)
                    .Select(sc => new
                    {
                        sc.Course.CourseName,
                        Предмет = sc.Course.Subject.SubjectName,
                        Преподаватель = sc.Course.Teacher.FullName,
                        sc.Course.StartDate,
                        sc.Course.EndDate
                    })
                    .ToList();

                dgMyCourses.ItemsSource = list;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}