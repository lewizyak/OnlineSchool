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
    public partial class TeacherCoursesWindow : Window
    {
        private int _teacherId;

        public TeacherCoursesWindow(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
            LoadCourses();
        }

        private void LoadCourses()
        {
            using (var db = new PasSchoolEntities1())
            {
                var list = db.Course
                    .Where(c => c.TeacherId == _teacherId)
                    .Select(c => new
                    {
                        c.CourseName,
                        Предмет = c.Subject.SubjectName,
                        c.StartDate,
                        c.EndDate,
                        c.MaxStudents,
                        Записано = db.StudentCourse.Count(sc => sc.CourseId == c.CourseId)
                    })
                    .ToList();

                dgCourses.ItemsSource = list;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}