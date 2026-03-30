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
    public partial class TeacherGradesWindow : Window
    {
        private int _teacherId;

        public TeacherGradesWindow(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
            LoadStudents();
        }

        private void LoadStudents()
        {
            using (var db = new PasSchoolEntities1())
            {
                var list = db.StudentCourse
                    .Where(sc => sc.Course.TeacherId == _teacherId)
                    .Select(sc => new
                    {
                        sc.StudentId,
                        sc.CourseId,
                        Студент = sc.Student.FullName,
                        Курс = sc.Course.CourseName,
                        Предмет = sc.Course.Subject.SubjectName
                    })
                    .ToList();

                dgStudents.ItemsSource = list;
            }
        }

        private void BtnSaveGrade_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem == null)
            {
                MessageBox.Show("Выбери студента");
                return;
            }

            int gradeValue;
            if (!int.TryParse(tbGrade.Text.Trim(), out gradeValue))
            {
                MessageBox.Show("Введи корректную оценку");
                return;
            }

            dynamic row = dgStudents.SelectedItem;
            int studentId = row.StudentId;
            int courseId = row.CourseId;
            string comment = tbComment.Text.Trim();

            using (var db = new PasSchoolEntities1())
            {
                Grade grade = new Grade
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    GradeValue = gradeValue,
                    Comment = comment,
                    GradeDate = DateTime.Now
                };

                db.Grade.Add(grade);
                db.SaveChanges();

                MessageBox.Show("Оценка сохранена");
                tbGrade.Clear();
                tbComment.Clear();
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
