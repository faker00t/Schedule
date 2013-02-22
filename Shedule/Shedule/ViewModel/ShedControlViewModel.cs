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
    //public class DisplayCurriculum
    //{
    //    public int _Regaction { get; set; }
    //    public string _Subject { get; set; }
    //    //public string _Group { get; set; }
    //    public string _Teacher { get; set; }
    //    public string _Type { get; set; }
    //}

    public class DisplayGroup
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
    }

    //public class DisplayLesson
    //{
    //    public string _Day { get; set; }
    //    public int _Number { get; set; }
    //    public string _Ring { get; set; }
    //    public int _Regaction { get; set; }
    //    public Ring _RingObj { get; set; }
    //}

    public class DisplayAuditorium
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
    }

    public class DisplayCurriculumLesson
    {
        public int _Regaction { get; set; }
        public string _Subject { get; set; }
        //public string _Group { get; set; }
        public string _Teacher { get; set; }
        public string _Type { get; set; }
        public int _Day { get; set; }
        public int _Number { get; set; }
        public int _Ring { get; set; }
        public Ring _RingObj { get; set; }
        public int _Hours { get; set; }
        public bool _Error { get; set; }
        public int _LessonID { get; set; }
        public bool _Flow { get; set; }
        public string _Auditorium { get; set; }
        public DateTime _Date { get; set; }

        public DisplayCurriculumLesson Copy()
        {
            DisplayCurriculumLesson copy = new DisplayCurriculumLesson();
            copy._Regaction = _Regaction;
            copy._Subject = _Subject;
            copy._Teacher = _Teacher;
            copy._Type = _Type;
            copy._Day = _Day;
            copy._Number = _Number;
            copy._Ring = _Ring;
            copy._RingObj = _RingObj;
            copy._Hours = _Hours;
            copy._Error = _Error;
            copy._LessonID = _LessonID;
            copy._Flow = _Flow;
            copy._Auditorium = _Auditorium;
            copy._Date = _Date;
            return copy;
        }
    }


    class ShedControlViewModel : ViewModelBase
    {
        private bool och; // очник 
        public bool Och
        {
            get { return och; }
            set
            {
                och = value;
                OnPropertyChanged("Och");
            }
        }

        private bool zaoch; // очник 
        public bool ZaOch
        {
            get { return zaoch; }
            set
            {
                zaoch = value;
                OnPropertyChanged("ZaOch");
            }
        }

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

        ObservableCollection<DisplayCurriculumLesson> curriculums;
        public ObservableCollection<DisplayCurriculumLesson> Curriculums
        {
            get { return curriculums; }
            set
            {
                curriculums = value;
                OnPropertyChanged("Curriculums");
            }
        }

        ObservableCollection<DisplayGroup> groups;
        public ObservableCollection<DisplayGroup> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
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

        ObservableCollection<DisplayAuditorium> auditoriums;
        public ObservableCollection<DisplayAuditorium> Auditoriums
        {
            get { return auditoriums; }
            set
            {
                auditoriums = value;
                OnPropertyChanged("Auditoriums");
            }
        }

        DisplayGroup selectedGroup;
        public DisplayGroup SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
                GroupSelectedHandler();
            }
        }

        DisplayCurriculumLesson selectedCurriculum;
        public DisplayCurriculumLesson SelectedCurriculum
        {
            get { return selectedCurriculum; }
            set
            {
                selectedCurriculum = value;
                OnPropertyChanged("SelectedCurriculum");
                CurriculumSelectedHandler();
            }
        }

        DisplayCurriculumLesson selectedLesson;
        public DisplayCurriculumLesson SelectedLesson
        {
            get { return selectedLesson; }
            set
            {
                selectedLesson = value;
                OnPropertyChanged("SelectedLesson");
                LessonSelectedHandler();
            }
        }
        public int SelectedLessonIndex { get; set; }

        string groupSeachField;
        public string GroupSeachField
        {
            get { return groupSeachField; }
            set
            {
                groupSeachField = value;
                OnPropertyChanged("GroupSeachField");
            }
        }

        string helpMessage;
        public string HelpMessage
        {
            get { return helpMessage; }
            set
            {
                helpMessage = value;
                OnPropertyChanged("HelpMessage");
            }
        }

        string errorInfo;
        public string ErrorInfo
        {
            get { return errorInfo; }
            set
            {
                errorInfo = value;
                OnPropertyChanged("ErrorInfo");
            }
        }

        string selectedbuilding;
        public string SelectedBuilding
        {
            get { return selectedbuilding; }
            set
            {
                selectedbuilding = value;
                OnPropertyChanged("SelectedBuilding");
            }
        }

        string[] days;

        #region дни недели
        public string Day1
        {
            get { return days[0]; }
            set
            {
                days[0] = value;
                OnPropertyChanged("Day1");
            }
        }

        public string Day2
        {
            get { return days[1]; }
            set
            {
                days[1] = value;
                OnPropertyChanged("Day2");
            }
        }

        public string Day3
        {
            get { return days[2]; }
            set
            {
                days[2] = value;
                OnPropertyChanged("Day3");
            }
        }

        public string Day4
        {
            get { return days[3]; }
            set
            {
                days[3] = value;
                OnPropertyChanged("Day4");
            }
        }

        public string Day5
        {
            get { return days[4]; }
            set
            {
                days[4] = value;
                OnPropertyChanged("Day5");
            }
        }

        public string Day6
        {
            get { return days[5]; }
            set
            {
                days[5] = value;
                OnPropertyChanged("Day6");
            }
        }

        public string Day7
        {
            get { return days[6]; }
            set
            {
                days[6] = value;
                OnPropertyChanged("Day7");
            }
        }
        #endregion

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

        string currentTeacher;

        public ShedControlViewModel()
        {
            days = new string[7];
            curriculums = new ObservableCollection<DisplayCurriculumLesson>();
            groups = new ObservableCollection<DisplayGroup>();
            lessons = new ObservableCollection<DisplayCurriculumLesson>();
            auditoriums = new ObservableCollection<DisplayAuditorium>();
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var cur = (from g in cnt.Groups select g);
                foreach (Group c in cur)
                {
                    DisplayGroup dispGroup = new DisplayGroup()
                    {
                        _Name = c.GroupAbbreviation,
                        _Id = c.Id,
                    };
                    Groups.Add(dispGroup);
                }
            }
            UpWeek = true;
            GroupSeachField = string.Empty;
            SelectedDate = DateTime.Today;
            ZaOch = true;
            Och = true;
        }

        #region загрузка расписвния для группы
        private DelegateCommand shedLoadByGroupCommand;

        public ICommand ShedLoadByGroupCommand
        {
            get
            {
                if (shedLoadByGroupCommand == null)
                {
                    shedLoadByGroupCommand = new DelegateCommand(ShedLoadByGroup);
                }
                return shedLoadByGroupCommand;
            }
        }
        public void ShedLoadByGroup()
        {
            Curriculums.Clear();
            Lessons.Clear();
            HelpMessage = "Перетягивайте элементы на расписание";
            if (SelectedGroup == null) return;
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                IEnumerable<Curriculum> cur = (from lt in cnt.Curriculums.Include("RegulatoryAction").Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.AcademicLoad.Employe") where lt.Group.Id == selectedGroup._Id select lt);
                foreach (Curriculum c in cur)
                {
                    var ra = c.RegulatoryAction;
                    var al = ra.AcademicLoad.First();
                    var e = al.Employe.Name;
                    var lt = ra.LessonsType.Name;
                    DisplayCurriculumLesson dispCurr = new DisplayCurriculumLesson()
                    {
                        _Subject = c.Subject.Name,
                        _Teacher = e,
                        _Type = lt,
                        _Regaction = ra.Id,
                        _Hours = ra.Hours,
                        // _Error = false,
                    };
                    Curriculums.Add(dispCurr);
                }

                int selectedWeekDayNumber = (int)selecteddate.DayOfWeek;
                if (selectedWeekDayNumber < 0) selectedWeekDayNumber = 6;
                DateTime startDay = selecteddate.AddDays(1-selectedWeekDayNumber);
                DateTime endDay = startDay.AddDays(7);

                for (int i = 1; i < 8; ++i) // lesson number
                {
                    //for (int j = 0; j < 7; ++j) // day
                    int j = 0; int count = 0;
                    for (DateTime day = startDay; day < endDay; day = day.AddDays(1))
                    {
                        ++count;
                        var lessons = (from l in cnt.Lessons where l.RingId == i && /*l.Day == j*/ l.Date == day /*&& l.Period == upweek*/ select l);
                        j = (int)day.DayOfWeek-1;
                        if (j < 0) j = 6;
                        days[j] = day.ToString("dddd d.MM.y");
                        OnPropertyChanged("Day" + (j+1).ToString());
                        Collection<Lesson> filtered_lessons = new Collection<Lesson>();
                        foreach (var c in cur) // отбросим пары других групп
                        {
                            foreach (var l in lessons)
                            {
                                if (c.RegulatoryActionId == l.RegulatoryActionId)
                                    filtered_lessons.Add(l);
                            }
                        }

                        DisplayCurriculumLesson dispLess = null;
                        if (filtered_lessons.Count() == 0)
                        {
                            dispLess = new DisplayCurriculumLesson()
                            {
                                //_Day = j,
                                //_Number = i,
                                //_Error = true,
                            };
                        }
                        else
                        {
                            var les = filtered_lessons.First();
                            var currsForLes = (from lt in cnt.Curriculums.Include("RegulatoryAction")
                                   .Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.AcademicLoad.Employe")
                                              where lt.RegulatoryActionId == les.RegulatoryActionId
                                              select lt);
                            var curForLes =currsForLes.First();
                            var ra = curForLes.RegulatoryAction;
                            var al = ra.AcademicLoad.First();
                            var e = al.Employe.Name;
                            var lesstype = ra.LessonsType.Name;
                            var auditorium = (from a in cnt.Auditoriums where a.Id == les.AuditoriumId select a).First();
                            bool flow = false;
                            if (currsForLes.Count() > 1) flow = true;
                            dispLess = new DisplayCurriculumLesson()
                            {
                                //_Day = j,
                                //_Number = i,
                                _Regaction = les.RegulatoryActionId,
                                _Subject = curForLes.Subject.Name,
                                _Teacher = e,
                                _Type = lesstype,
                                //_Error = false,
                                _LessonID = les.Id,
                                _Flow = flow,
                                _Auditorium = "ауд. "+auditorium.Building+"-"+auditorium.Number,
                                _Day = les.Day,
                                _Number = les.RingId,
                            };

                        }
                        Lessons.Add(dispLess);
                    }
                    //MessageBox.Show(count.ToString());
                }
            }
        }
        #endregion

        #region сохранение расписания группы
        private DelegateCommand saveshed;

        public ICommand SaveShedCommand
        {
            get
            {
                if (saveshed == null)
                {
                    saveshed = new DelegateCommand(SaveShed);
                }
                return saveshed;
            }
        }
        public void SaveShed()
        {
            bool saved_ok = true;
            int lessonNumber = 0;
            //
            int i = 0;
            foreach (var l in lessons)
            {
                using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
                {
                    var res = (from c in cnt.Curriculums where c.RegulatoryActionId == l._Regaction select c);
                    if (res.Count() > 1)
                    {
                        // это поток, проверить все группы..
                        int day;
                        int number;
                        //MessageBox.Show("index = " + i);
                        indexToNumberDay(i, out number, out day);
                        if (l._Number != number || l._Day != day)
                            saved_ok = CheckFlowOk(res.First().RegulatoryActionId,number,day);
                        //MessageBox.Show("flow number "+number.ToString()+" day "+day.ToString());
                    }
                }
                ++i;
            }

            if (!saved_ok)
            {
                ErrorInfo = "В расписании имеются накладки! Исправте их и попробуйте снова!";
                return;
            }

            foreach (var s in lessons)
            {
                if (s._Regaction != 0)
                {
                    Console.WriteLine(s._Regaction.ToString());
                    using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
                    {
                        var checklessons = (from l in cnt.Lessons where l.RegulatoryActionId == s._Regaction && l.Period == upweek select l);
                        if (checklessons.Count() == 0)
                        {
                            //var regaction = (from r in cnt.RegulatoryActions where r.Id == s._Regaction select r).First();
                            Console.WriteLine("1) Number = {0} Day = {1}", (int)(lessonNumber / 7) + 1, lessonNumber % 7);
                            Lesson newLesson = new Lesson()
                            {
                                RingId = (lessonNumber / 7) + 1,
                                RegulatoryActionId = s._Regaction,
                                //Day = (lessonNumber % 7), // для заочников пока убрал
                                Period = upweek,
                                AuditoriumId = 1,
                                Date = WeekDayNumberToDay(lessonNumber % 7), // для заочников получаем дату заниятия
                            };
                            cnt.Lessons.AddObject(newLesson);
                        }
                        else
                        {
                            var les = checklessons.First();
                            Console.WriteLine("2) Number = {0} Day = {1}", (int)(lessonNumber / 7) + 1, lessonNumber % 7);
                            les.RingId = (lessonNumber / 7) + 1;
                            les.RegulatoryActionId = s._Regaction;
                            //les.Day = (lessonNumber % 7);
                            les.Period = upweek;
                            les.AuditoriumId = 1;
                            les.Date = WeekDayNumberToDay(lessonNumber % 7);
                            cnt.Refresh(System.Data.Objects.RefreshMode.ClientWins, les);
                        }
                        cnt.SaveChanges();
                    }
                }
                ++lessonNumber;
            }
            if (saved_ok) ShedLoadByGroup();
        }

        #endregion

        #region Выбор файла
        private DelegateCommand groupsearch;

        public ICommand GroupSearchCommand
        {
            get
            {
                if (groupsearch == null)
                {
                    groupsearch = new DelegateCommand(GroupSearch);
                }
                return groupsearch;
            }
        }
        public void GroupSearch()
        {
            Groups.Clear();
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                
                IEnumerable<Group> cur = null;
                if (zaoch && och) cur = (from g in cnt.Groups where (SqlFunctions.PatIndex("%" + GroupSeachField + "%", g.GroupAbbreviation) > 0) select g);
                else if (zaoch) cur = (from g in cnt.Groups where (SqlFunctions.PatIndex("%" + GroupSeachField + "%", g.GroupAbbreviation) > 0) && g.StudyTypeId == 2 select g);
                else if (och) cur = (from g in cnt.Groups where (SqlFunctions.PatIndex("%" + GroupSeachField + "%", g.GroupAbbreviation) > 0) && g.StudyTypeId == 1 select g);
                else return;

                foreach (Group c in cur)
                {
                    DisplayGroup dispGroup = new DisplayGroup()
                    {
                        _Name = c.GroupAbbreviation,
                        _Id = c.Id,
                    };
                    Groups.Add(dispGroup);
                }
            }
        }
        #endregion

        void CurriculumSelectedHandler()
        {
            if (SelectedCurriculum == null) return;
            ErrorInfo = string.Empty;
            currentTeacher = SelectedCurriculum._Teacher;

            if (SelectedCurriculum._Hours < 80)
                HelpMessage = "Необходимо назначить 4 пары в две недели";
            if (SelectedCurriculum._Hours < 60)
                HelpMessage = "Необходимо назначить 3 пары в две недели";
            if (SelectedCurriculum._Hours < 40)
                HelpMessage = "Необходимо назначить 2 пары в две недели";
            if (SelectedCurriculum._Hours < 20)
                HelpMessage = "Необходимо назначить 1 пары в две недели";

            foreach (var l in Lessons)
            {
                l._Error = false;
            }

            List<int> found = CheckTeacher((upweek) ? 0 : 1, SelectedCurriculum._Teacher);
            foreach (var i in found)
            {
                Lessons[i]._Error = true;
                //l._Regaction = found;
            }

            var tmp = new ObservableCollection<DisplayCurriculumLesson>(lessons);
            Lessons = tmp;
        }

        void LessonSelectedHandler()
        {
            CheckLesson();
            //var tmp = new ObservableCollection<DisplayCurriculumLesson>(lessons);
            //Lessons = tmp;
        }

        List<int> CheckTeacher(int weekType, string teacherName)
        {
            //int found = 0;
            int number;
            int day;
            List<int> busyLessons = new List<int>();
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                //ищем преподавателя по имени
                var teacher = (from c in cnt.Employees where c.Name == teacherName select c).First();
                //смотрим какие мероприятия его
                var teachersAcademicLoad = (from r in cnt.AcademicLoadSet where r.Employe.Id == teacher.Id select r);
                foreach (var tAL in teachersAcademicLoad)
                {
                    //выбираем мероприятия преподавателя
                    var teacherLessons = (from l in cnt.Lessons where tAL.RegulatoryAction.Id == l.RegulatoryAction.Id select l);
                    foreach (var tL in teacherLessons)
                    {
                        for (int i = 0; i < 49; ++i)
                        {
                            indexToNumberDay(i, out number, out day);
                            if (number == tL.RingId && day == tL.Day && tL.Period == upweek)
                            {
                                var res = (from r in cnt.Curriculums where r.RegulatoryActionId == tL.RegulatoryActionId select r);
                                if (res.Count() == 1)
                                {
                                    if (SelectedGroup._Id != res.First().GroupId)
                                        busyLessons.Add(i);// = tL.RegulatoryActionId;
                                }
                                else if (res.Count() > 1)
                                {
                                    bool inFlow = false;
                                    foreach (var c in res)
                                    {
                                        if (SelectedGroup._Id == c.GroupId) inFlow = true;
                                    }
                                    if (!inFlow) busyLessons.Add(i); //found = tL.RegulatoryActionId;
                                }
                            }
                        }

                    }
                }
            }
            return busyLessons;
        }

        void indexToNumberDay(int i, out int number, out int day)
        {
            number = (int)(i / 7) + 1;
            day = (i % 7);
        }

        void CheckLesson()
        {
            int number;
            int day;
            int regaction = 0;

            if (SelectedLesson == null) return;

            ErrorInfo = string.Empty;
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                if (SelectedLesson._Error || SelectedLesson._Flow)
                {
                    Employe teacher = null;
                    if (SelectedLesson._Error)
                        teacher = (from c in cnt.Employees where c.Name == SelectedCurriculum._Teacher select c).First(); //sel curr
                    else
                        teacher = (from c in cnt.Employees where c.Name == SelectedLesson._Teacher select c).First(); //sel curr
                    var teachersAcademicLoad = (from r in cnt.AcademicLoadSet where r.Employe.Id == teacher.Id select r);
                    bool flag = false;
                    foreach (var tAL in teachersAcademicLoad)
                    {
                        if (flag) break;
                        //выбираем мероприятия преподавателя
                        var teacherLessons = (from l in cnt.Lessons where tAL.RegulatoryAction.Id == l.RegulatoryAction.Id select l);
                        foreach (var tL in teacherLessons)
                        {
                            indexToNumberDay(SelectedLessonIndex, out number, out day);
                            if (number == tL.RingId && day == tL.Day && tL.Period == upweek)
                            {
                                regaction = tL.RegulatoryActionId;
                                flag = true;
                                break;
                            }

                        }
                    }

                    //if (SelectedLesson._Flow) regaction = SelectedLesson._Regaction;

                    var res = (from r in cnt.Curriculums where r.RegulatoryActionId == regaction select r);
                    if (res.Count() == 1)
                    {
                        var firstRes = res.First();
                        ErrorInfo = "Внимание! " + currentTeacher + " уже ведёт пару в это время у группы " + firstRes.Group.GroupAbbreviation;
                    }
                    else if (res.Count() > 1)
                    {
                        ErrorInfo += "Внимание! Эта пара определена для потока! (";
                        string flowGroups = string.Empty;
                        foreach (var c in res)
                        {
                            flowGroups += c.Group.GroupAbbreviation + " ";
                        }
                        ErrorInfo += flowGroups.TrimEnd() + ")";
                    }
                }
            }
        }

        bool CheckFlowOk(int regaction, int number, int day)
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var currs = (from r in cnt.Curriculums where r.RegulatoryActionId == regaction select r);
                foreach (var cur in currs)
                {
                    //проверим, свободно ли время для каждой групы из потока
                    int groupId = cur.GroupId;
                    var currsForFlowGroup = (from c in cnt.Curriculums where c.GroupId == groupId select c);
                    foreach (var currForFlowGroup in currsForFlowGroup)
                    {
                        var res = (from l in cnt.Lessons where l.RingId == number && l.Day == day && l.Period == upweek && currForFlowGroup.RegulatoryActionId == l.RegulatoryActionId select l);
                        if (res.Count() > 0)
                        {
                            //MessageBox.Show(res.First().RegulatoryActionId.ToString());
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        void ChangeWeekTypeHandler()
        {
            ShedLoadByGroup();
        }

        void DateSelectedHandler()
        {
            ShedLoadByGroup();
        }

        void GroupSelectedHandler()
        {
            ShedLoadByGroup();
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

        DateTime WeekDayNumberToDay(int daynum) // преобразует день недели от 0 до 6 (пн-вс) в дату, на основе выбранной в датапикере даты
        {
            int selday = (int)selecteddate.DayOfWeek;
            DateTime Date = selecteddate.AddDays(daynum+1-selday);
            return Date;
        }

        #region загрузка аудиторий
        private DelegateCommand loadauditoriums;

        public ICommand LoadAuditoriumsCommand
        {
            get
            {
                if (loadauditoriums == null)
                {
                    loadauditoriums = new DelegateCommand(LoadAuditoriums);
                }
                return loadauditoriums;
            }
        }
        public void LoadAuditoriums()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var currs = (from r in cnt.Auditoriums /*where r.Building == selectedbuilding*/ select r);
                Auditoriums.Clear();// = new ObservableCollection<DisplayAuditorium>();
                foreach (var cur in currs)
                {
                    Auditoriums.Add(new DisplayAuditorium()
                    {
                        _Id = cur.Id,
                        _Name = cur.Building.ToString() + "-" + cur.Number.ToString(),
                    }
                    );
                }
            }
        }
        #endregion

    }
}
