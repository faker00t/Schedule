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
using Shedule.Common;

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
            lessons = new ObservableCollection<DisplayCurriculumLesson>();
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
            lessons.Clear();
            for (int i = 0; i < 49; ++i)
            {
                lessons.Add(new DisplayCurriculumLesson());
            }
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var les = (from l in cnt.Lessons.Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.Curriculum") select l);
                foreach (var l in les)
                {
                    foreach (var a in l.RegulatoryAction.AcademicLoad)
                    {
                        if (a.EmployeId == selectedTeacher.Id)
                        {
                            int i = l.Day + (l.RingId - 1) * 7;
                            lessons[i]._Subject = l.RegulatoryAction.Curriculum.First().Subject.Name;
                            foreach (var c in l.RegulatoryAction.Curriculum)
                            {
                                lessons[i]._Group += c.Group.GroupAbbreviation + " ";
                            }
                            break;
                        }
                    }
                }
            }

            Lessons = new ObservableCollection<DisplayCurriculumLesson>(lessons);
        }

        private void TeacherSelectedHandler()
        {
            FillLessonsList();
        }
    }
}
