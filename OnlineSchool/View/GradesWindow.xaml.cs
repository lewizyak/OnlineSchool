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
    /// Логика взаимодействия для GradesWindow.xaml
    /// </summary>
    public partial class GradesWindow : Window
    {
        private int _studentId;

        public GradesWindow(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;

            LoadGrades();
        }

        private void LoadGrades()
        {
            using (var db = new PasSchoolEntities())
            {
                var list = db.Grade
                    .Where(g => g.StudentId == _studentId)
                    .Select(g => new
                    {
                        g.Course.CourseName,
                        Оценка = g.GradeValue,
                        Комментарий = g.Comment,
                        g.GradeDate
                    })
                    .ToList();

                dgGrades.ItemsSource = list;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
