using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;

using Shedule.Commands;
using Shedule.Import;
using Shedule.Data;
using System.Collections.ObjectModel;
using System.Data.Objects.SqlClient;

namespace Shedule.ViewModel
{
    class ShedTeacherViewModel : ViewModelBase
    {
        ObservableCollection<Employe> teachers;
        public ObservableCollection<Employe> Teachers
        {
            get { return teachers; }
            set
            {
                teachers = value;
                OnPropertyChanged("Teachers");
            }
        }

        ObservableCollection<DisplayCurriculumLesson> lessons;
        public ObservableCollection<DisplayCurriculumLesson> Lessons
        {
            get { return lessons; }
            set
            {
                lessons = value;
                OnPropertyChanged("Lessons");
            }
        }

        Employe selectedTeacher;
        public Employe SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                selectedTeacher = value;
                OnPropertyChanged("SelectedTeacher");
                TeacherSelectedHandler();
            }
        }

        /// <summary>
        /// 
        /// </summary>

        public ShedTeacherViewModel()
        {
            FillTeachersList();
        }

        private void FillTeachersList()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var tea = (from t in cnt.Employees select t).OrderBy(t => t.Name);
                Teachers = new ObservableCollection<Employe>(tea);
            }
        }

        private void FillLessonsList()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
            }
        }

        private void TeacherSelectedHandler()
        {
            FillLessonsList();
        }
    }
}
