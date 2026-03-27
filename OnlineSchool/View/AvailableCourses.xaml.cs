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
    /// Логика взаимодействия для AvailableCourses.xaml
    /// </summary>
    public partial class AvailableCourses : Window
    {
        private int _studentId;

        public AvailableCourses(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;

            LoadCourses();
        }

        private void LoadCourses()
        {
            using (var db = new PasSchoolEntities())
            {
                var list = db.Course
                    .Where(c => !db.StudentCourse.Any(sc => sc.StudentId == _studentId && sc.CourseId == c.CourseId))
                    .Select(c => new
                    {
                        c.CourseId,
                        c.CourseName,
                        Предмет = c.Subject.SubjectName,
                        Преподаватель = c.Teacher.FullName,
                        c.StartDate,
                        c.EndDate,
                        c.MaxStudents,
                        Записано = db.StudentCourse.Count(sc => sc.CourseId == c.CourseId)
                    })
                    .Where(x => x.Записано < x.MaxStudents)
                    .ToList();

                dgAvailableCourses.ItemsSource = list;
            }
        }

        private void BtnJoin_Click(object sender, RoutedEventArgs e)
        {
            if (dgAvailableCourses.SelectedItem == null)
            {
                MessageBox.Show("Выбери курс");
                return;
            }

            dynamic row = dgAvailableCourses.SelectedItem;
            int courseId = row.CourseId;

            using (var db = new PasSchoolEntities())
            {
                bool exists = db.StudentCourse.Any(sc => sc.StudentId == _studentId && sc.CourseId == courseId);

                if (exists)
                {
                    MessageBox.Show("Ты уже записан на этот курс");
                    return;
                }

                int currentCount = db.StudentCourse.Count(sc => sc.CourseId == courseId);
                int maxCount = db.Course.Where(c => c.CourseId == courseId).Select(c => c.MaxStudents).FirstOrDefault();

                if (currentCount >= maxCount)
                {
                    MessageBox.Show("На курсе уже нет свободных мест");
                    LoadCourses();
                    return;
                }

                StudentCourse studentCourse = new StudentCourse
                {
                    StudentId = _studentId,
                    CourseId = courseId
                };

                db.StudentCourse.Add(studentCourse);
                db.SaveChanges();

                MessageBox.Show("Запись на курс выполнена");
                LoadCourses();
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCourses();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
