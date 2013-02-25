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
    class ShedAuditoriumViewModel : ViewModelBase
    {
         #region тип недели, дата
        private bool upweek;
        public bool UpWeek
        {
            get { return upweek; }
            set
            {
                upweek = value;
                OnPropertyChanged("UpWeek");
                ChangeWeekTypeHandler();
            }
        }

        private bool downweek;
        public bool DownWeek
        {
            get { return downweek; }
            set
            {
                downweek = value;
                OnPropertyChanged("DownWeek");
            }
        }

        DateTime selecteddate;
        public DateTime SelectedDate
        {
            get { return selecteddate; }
            set
            {
                selecteddate = value;
                OnPropertyChanged("SelectedDate");
                DateSelectedHandler();
            }
        }
        #endregion

        ObservableCollection<Auditorium> auditorium;
        public ObservableCollection<Auditorium> Auditoriums
        {
            get { return auditorium; }
            set
            {
                auditorium = value;
                OnPropertyChanged("Auditoriums");
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

        Auditorium selectedAuditorium;
        public Auditorium SelectedAuditorium
        {
            get { return selectedAuditorium; }
            set
            {
                selectedAuditorium = value;
                OnPropertyChanged("SelectedAuditorium");
                AuditoriumSelectedHandler();
            }
        }

        /// <summary>
        /// 
        /// </summary>

        public ShedAuditoriumViewModel()
        {
            lessons = new ObservableCollection<DisplayCurriculumLesson>();
            UpWeek = true;
            SelectedDate = DateTime.Today;
            FillAuditoriumsList();
        }

        private void FillAuditoriumsList()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var aud = (from a in cnt.Auditoriums.Include("Department") select a).OrderBy(a => a.Building);
                Auditoriums = new ObservableCollection<Auditorium>(aud);
            }
        }

        private void FillLessonsList()
        {
            lessons.Clear();
            for (int i = 0; i < 49; ++i)
            {
                lessons.Add(new DisplayCurriculumLesson());
            }

            if (selectedAuditorium == null) return;

            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var les = (from l in cnt.Lessons.Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.Curriculum") where l.Period == upweek && l.AuditoriumId == selectedAuditorium.Id select l);
                foreach (var l in les)
                {
                    int i = HelperClasses.numberDayToIndex(l.Day, l.RingId);
                    lessons[i]._Subject = l.RegulatoryAction.Curriculum.First().Subject.Name;
                    lessons[i]._Teacher = l.RegulatoryAction.AcademicLoad.First().Employe.Name;
                    foreach (var c in l.RegulatoryAction.Curriculum)
                    {
                        lessons[i]._Group += c.Group.GroupAbbreviation + " ";
                    }
                }
            }

            Lessons = new ObservableCollection<DisplayCurriculumLesson>(lessons);
        }

        #region неделя -
        private DelegateCommand prevweek;

        public ICommand PrevWeekCommand
        {
            get
            {
                if (prevweek == null)
                {
                    prevweek = new DelegateCommand(PrevWeek);
                }
                return prevweek;
            }
        }
        public void PrevWeek()
        {
            SelectedDate = SelectedDate.AddDays(-7);
        }
        #endregion

        #region неделя +
        private DelegateCommand nextweek;

        public ICommand NextWeekCommand
        {
            get
            {
                if (nextweek == null)
                {
                    nextweek = new DelegateCommand(NextWeek);
                }
                return nextweek;
            }
        }
        public void NextWeek()
        {
            SelectedDate = SelectedDate.AddDays(7);
        }
        #endregion

        #region обработчики на изменении свойств
        private void AuditoriumSelectedHandler()
        {
            FillLessonsList();
        }

        private void ChangeWeekTypeHandler()
        {
            FillLessonsList();
        }

        private void DateSelectedHandler()
        {
        }
        #endregion
    }
}
