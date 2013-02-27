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

        ObservableCollection<Group> groups;
        public ObservableCollection<Group> Groups
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

        ObservableCollection<Auditorium> auditoriums;
        public ObservableCollection<Auditorium> Auditoriums
        {
            get { return auditoriums; }
            set
            {
                auditoriums = value;
                OnPropertyChanged("Auditoriums");
            }
        }

        Group selectedGroup;
        public Group SelectedGroup
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

        int selectedbuilding;
        public int SelectedBuilding
        {
            get { return selectedbuilding; }
            set
            {
                selectedbuilding = value;
                OnPropertyChanged("SelectedBuilding");
                BuildingSelectedHandler();
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

        Auditorium selectedauditorium;
        public Auditorium SelectedAuditorium
        {
            get { return selectedauditorium; }
            set
            {
                selectedauditorium = value;
                OnPropertyChanged("SelectedAuditorium");
            }
        }

        string currentTeacher;

        public ShedControlViewModel()
        {
            days = new string[7];
            curriculums = new ObservableCollection<DisplayCurriculumLesson>();
            groups = new ObservableCollection<Group>();
            lessons = new ObservableCollection<DisplayCurriculumLesson>();
            auditoriums = new ObservableCollection<Auditorium>();
            UpWeek = true;
            GroupSeachField = string.Empty;
            SelectedDate = DateTime.Today;
            SelectedBuilding = 0;
            ZaOch = true;
            Och = true;
            GroupSearch(); // выводим список групп
        }

        #region загрузка расписания для группы
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
                // узнаем форму обучения для группы
                var group = (from g in cnt.Groups where g.Id == selectedGroup.Id select g).First();
                int studytype = group.StudyTypeId;
                //поставим начало срока обучения
                if (group.EduPeriod.Count > 0) selecteddate = group.EduPeriod.First().Begin; // не обновится на форме..

                // получим учебный план группы
                IEnumerable<Curriculum> cur = (from lt in cnt.Curriculums.Include("RegulatoryAction").Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.AcademicLoad.Employe") where lt.Group.Id == selectedGroup.Id select lt);
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

                // узнаем срок обучения группы чтобы не прокручивать расписание за его пределы
                // .. откуда только?..
                // ----------------------------------------

                // в зависимости от формы обучения выводим расписание
                // для заочников ищем по дате
                // для очников по дню недели

                int selectedWeekDayNumber = (int)selecteddate.DayOfWeek;
                DateTime startDay = selecteddate.AddDays(1 - selectedWeekDayNumber); // начнём неделю с понедельника (а не с воскресенья)
                DateTime endDay = startDay.AddDays(7);

                if (studytype == 1) // очная
                {
                    // заполняем табличку расписания для очников
                    for (int i = 1; i < 8; ++i) // lesson number
                    {
                        for (int j = 0; j < 7; ++j) // day
                        {
                            IEnumerable<Lesson> lessons = (from l in cnt.Lessons where l.RingId == i && l.Day == j && l.Period == upweek select l);
                            /// придумать вывод расписания от дня недели с определение типа недели (верх/низ)

                            /*j = (int)day.DayOfWeek - 1;
                            if (j < 0) j = 6;
                            days[j] = day.ToString("dddd d.MM.y");
                            OnPropertyChanged("Day" + (j + 1).ToString());*/
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
                                dispLess = new DisplayCurriculumLesson();
                            }
                            else
                            {
                                var les = filtered_lessons.First();
                                var currsForLes = (from lt in cnt.Curriculums.Include("RegulatoryAction")
                                       .Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.AcademicLoad.Employe")
                                                   where lt.RegulatoryActionId == les.RegulatoryActionId
                                                   select lt);
                                var curForLes = currsForLes.First();
                                var ra = curForLes.RegulatoryAction;
                                var al = ra.AcademicLoad.First();
                                var e = al.Employe.Name;
                                var lesstype = ra.LessonsType.Name;
                                var auditorium = (from a in cnt.Auditoriums where a.Id == les.AuditoriumId select a).First();
                                bool flow = false;
                                if (currsForLes.Count() > 1) flow = true;
                                dispLess = new DisplayCurriculumLesson()
                                {
                                    _Regaction = les.RegulatoryActionId,
                                    _Subject = curForLes.Subject.Name,
                                    _Teacher = e,
                                    _Type = lesstype,
                                    //_Error = false,
                                    _LessonID = les.Id,
                                    _Flow = flow,
                                    _Auditorium = "ауд. " + auditorium.Number,
                                    _AudID = auditorium.Id,
                                    _Day = les.Day,
                                    _Number = les.RingId,
                                };

                            }
                            Lessons.Add(dispLess);
                        }
                    }
                }
                else if (studytype == 2) // заочная
                {
                    // заполняем табличку расписания для заочников
                    for (int i = 1; i < 8; ++i) // lesson number
                    {
                        //for (int j = 0; j < 7; ++j) // day
                        int j = 0;
                        for (DateTime day = startDay; day < endDay; day = day.AddDays(1))
                        {
                            IEnumerable<Lesson> lessons = (from l in cnt.Lessons where l.RingId == i && /*l.Day == j*/ l.Date == day /*&& l.Period == upweek*/ select l);
                            j = (int)day.DayOfWeek - 1;
                            if (j < 0) j = 6;
                            days[j] = day.ToString("dddd d.MM.y");
                            OnPropertyChanged("Day" + (j + 1).ToString());
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
                                dispLess = new DisplayCurriculumLesson();
                            }
                            else
                            {
                                var les = filtered_lessons.First();
                                var currsForLes = (from lt in cnt.Curriculums.Include("RegulatoryAction")
                                       .Include("RegulatoryAction.AcademicLoad").Include("RegulatoryAction.AcademicLoad.Employe")
                                                   where lt.RegulatoryActionId == les.RegulatoryActionId
                                                   select lt);
                                var curForLes = currsForLes.First();
                                var ra = curForLes.RegulatoryAction;
                                var al = ra.AcademicLoad.First();
                                var e = al.Employe.Name;
                                var lesstype = ra.LessonsType.Name;
                                var auditorium = (from a in cnt.Auditoriums where a.Id == les.AuditoriumId select a).First();
                                bool flow = false;
                                if (currsForLes.Count() > 1) flow = true;
                                dispLess = new DisplayCurriculumLesson()
                                {
                                    _Regaction = les.RegulatoryActionId,
                                    _Subject = curForLes.Subject.Name,
                                    _Teacher = e,
                                    _Type = lesstype,
                                    //_Error = false,
                                    _LessonID = les.Id,
                                    _Flow = flow,
                                    _Auditorium = "ауд. " + auditorium.Number,
                                    _AudID = auditorium.Id,
                                    _Day = les.Day,
                                    _Number = les.RingId,
                                };

                            }
                            Lessons.Add(dispLess);
                        }
                    }
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
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                // узнаем форму обучения для группы
                int studytype = (from g in cnt.Groups where g.Id == selectedGroup.Id select g.StudyTypeId).First();

                foreach (var l in lessons) // проходим по расписанию на экране
                {
                    var res = (from c in cnt.Curriculums where c.RegulatoryActionId == l._Regaction select c);
                    if (res.Count() > 1)
                    {
                        // это поток, проверить все группы..
                        int day;
                        int number;
                        //MessageBox.Show("index = " + i);
                        HelperClasses.indexToNumberDay(i, out number, out day);
                        if (l._Number != number || l._Day != day) // мы передвинули день?
                            saved_ok = CheckFlowOk(res.First().RegulatoryActionId, number, day);
                        //MessageBox.Show("flow number "+number.ToString()+" day "+day.ToString());
                    }
                }
                ++i;


                if (!saved_ok)
                {
                    ErrorInfo = "В расписании имеются накладки! Исправте их и попробуйте снова!";
                    return;
                }

                foreach (var s in lessons) // проходим по расписанию на экране
                {
                    if (s._Regaction != 0) // если в ячейке есть пара
                    {
                        //Console.WriteLine(s._Regaction.ToString());

                        var checklessons = (from l in cnt.Lessons where l.RegulatoryActionId == s._Regaction && l.Period == upweek select l);

                        /// !!!!а нужна ли эта проверка???????????????????????
                        /// можно просто всегда добавлять новую пару и предупредить о превышении часов если что...
                        if (checklessons.Count() == 0) // если пары с таким ID ещё не было в расписании
                        {
                            //var regaction = (from r in cnt.RegulatoryActions where r.Id == s._Regaction select r).First();
                            //Console.WriteLine("1) Number = {0} Day = {1}", (int)(lessonNumber / 7) + 1, lessonNumber % 7);

                            Lesson newLesson = new Lesson();
                            newLesson.RingId = (lessonNumber / 7) + 1;
                            newLesson.RegulatoryActionId = s._Regaction;
                            newLesson.Period = upweek;
                            newLesson.AuditoriumId = s._AudID;
                            if (studytype == 1) // очник
                            {
                                newLesson.Day = (lessonNumber % 7); // для заочников пока убрал
                                //Date = WeekDayNumberToDay(lessonNumber % 7), // для заочников получаем дату занятия
                                newLesson.Date = DateTime.Now; // дата не может быть пустой!! запишем что нибудь туда..
                            }
                            else if (studytype == 2) // заочник
                            {
                                newLesson.Date = HelperClasses.WeekDayNumberToDay(lessonNumber % 7, selecteddate); // для заочников получаем дату занятия
                            }
                            cnt.Lessons.AddObject(newLesson);
                        }
                        else // если мы её уже назанчали
                        {
                            var les = checklessons.First();
                            //Console.WriteLine("2) Number = {0} Day = {1}", (int)(lessonNumber / 7) + 1, lessonNumber % 7);
                            les.RingId = (lessonNumber / 7) + 1;
                            les.RegulatoryActionId = s._Regaction;
                            les.Period = upweek;
                            les.AuditoriumId = s._AudID;
                            if (studytype == 1) // очник
                            {
                                les.Day = (lessonNumber % 7);
                                les.Date = DateTime.Now;
                            }
                            else if (studytype == 2) // заочник
                            {
                                les.Date = HelperClasses.WeekDayNumberToDay(lessonNumber % 7,selecteddate);
                            }
                            cnt.Refresh(System.Data.Objects.RefreshMode.ClientWins, les);
                        }
                        cnt.SaveChanges();

                    }
                    ++lessonNumber;
                }
            }

            if (saved_ok) ShedLoadByGroup();
        }

        #endregion

        #region поиск по группе
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
                if (zaoch && och) cur = (from g in cnt.Groups.Include("Faculty").Include("StudyType").Include("FieldOfStudy") where (SqlFunctions.PatIndex("%" + GroupSeachField + "%", g.GroupAbbreviation) > 0) select g);
                else if (zaoch) cur = (from g in cnt.Groups.Include("Faculty").Include("StudyType").Include("FieldOfStudy") where (SqlFunctions.PatIndex("%" + GroupSeachField + "%", g.GroupAbbreviation) > 0) && g.StudyTypeId == 2 select g);
                else if (och) cur = (from g in cnt.Groups.Include("Faculty").Include("StudyType").Include("FieldOfStudy") where (SqlFunctions.PatIndex("%" + GroupSeachField + "%", g.GroupAbbreviation) > 0) && g.StudyTypeId == 1 select g);
                else return;

                Groups = new ObservableCollection<Group>(cur);
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

            // закрасим клетки, когда препод занят
            List<int> found = CheckTeacher((upweek) ? 0 : 1, SelectedCurriculum._Teacher);
            foreach (var i in found)
            {
                Lessons[i]._Error = true;
                //l._Regaction = found;
            }
        }

        void LessonSelectedHandler()
        {
            CheckLesson();
        }

        List<int> CheckTeacher(int weekType, string teacherName)
            // возвращает номера клеток, когда преподаватель занят
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
                            HelperClasses.indexToNumberDay(i, out number, out day);
                            if (number == tL.RingId && day == tL.Day && tL.Period == upweek)
                            {
                                var res = (from r in cnt.Curriculums where r.RegulatoryActionId == tL.RegulatoryActionId select r);
                                if (res.Count() == 1)
                                {
                                    if (SelectedGroup.Id != res.First().GroupId)
                                        busyLessons.Add(i);
                                }
                                else if (res.Count() > 1)
                                {
                                    bool inFlow = false; // не предупреждать, если это поток с выбранной группой
                                    foreach (var c in res)
                                    {
                                        if (SelectedGroup.Id == c.GroupId) inFlow = true;
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

        void CheckLesson() // НЕ РАБОТАЕТ ДЛЯ ЗАОЧНИКОВ!!!!!!!!!!!!
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
                    else // т.е. это поток
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
                            HelperClasses.indexToNumberDay(SelectedLessonIndex, out number, out day);
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
                    //проверим, свободно ли время для каждой группы из потока
                    int groupId = cur.GroupId;
                    var currsForFlowGroup = (from c in cnt.Curriculums where c.GroupId == groupId select c);
                    foreach (var currForFlowGroup in currsForFlowGroup)
                    {
                        var res = (from l in cnt.Lessons where l.RingId == number && l.Day == day && l.Period == upweek && currForFlowGroup.RegulatoryActionId == l.RegulatoryActionId select l);
                        if (res.Count() > 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool CheckAuditorium(int audID, int number, int day, bool weektype)
            // проверяет, свободна ли аудитория в заданное время
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var lessons = (from l in cnt.Lessons where l.AuditoriumId == audID && l.RingId == number && l.Day == day && l.Period == upweek select l);
                if (lessons.Count() > 0) return false;
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
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                if (selectedGroup == null) return;
                var group = (from g in cnt.Groups where g.Id == selectedGroup.Id select g).First();
                if (group.EduPeriod.Count > 0) // если задан период обучения
                {
                    if (selecteddate < group.EduPeriod.First().Begin || selecteddate > group.EduPeriod.First().End)
                    {
                        SelectedDate = group.EduPeriod.First().Begin;
                        return;
                    }
                }
            }
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
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                if (selectedGroup == null) return;
                var group = (from g in cnt.Groups where g.Id == selectedGroup.Id select g).First();
                if (group.EduPeriod.Count > 0) // если задан период обучения
                {
                    if (selecteddate < group.EduPeriod.First().Begin || selecteddate > group.EduPeriod.First().End)
                    {
                        SelectedDate = group.EduPeriod.First().End;
                        return;
                    }
                }
            }
            SelectedDate = SelectedDate.AddDays(7);
        }
        #endregion

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
                IEnumerable<Auditorium> audit = null;
                if (selectedbuilding > 0)
                {
                    audit = (from r in cnt.Auditoriums.Include("Department") where r.Building == selectedbuilding select r);
                }
                else
                {
                    audit = (from r in cnt.Auditoriums.Include("Department") select r);
                }
                Auditoriums = new ObservableCollection<Auditorium>(audit);
            }
        }
        #endregion

        void BuildingSelectedHandler()
        {
            LoadAuditoriums();
        }

        #region назначить аудиторию для выделенной пары
        private DelegateCommand setauditorium;

        public ICommand SetAuditoriumCommand
        {
            get
            {
                if (setauditorium == null)
                {
                    setauditorium = new DelegateCommand(SetAuditorium);
                }
                return setauditorium;
            }
        }
        public void SetAuditorium()
        {
            /// !!!следует также учитывать тип занятия, но для аудиторий его негде взять
            int number;
            int day;
            int i = 0;
            bool small = false; // недостаточно мест
            bool empty; // свободна в это время
            string msg = string.Empty;

            foreach (var l in lessons)
            {
                if (l == selectedLesson) break;
                ++i;
            }

            HelperClasses.indexToNumberDay(i,out number,out day);
            empty = CheckAuditorium(selectedauditorium.Id, number, day, upweek);
            if (!empty)
            {
                msg += "Аудитория занята! ";
            }

            if (selectedLesson._Flow)
            {
                // проверить чтобы поместился поток
                int students = 0;
                using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
                {
                    var res = (from c in cnt.Curriculums.Include("Group") where c.RegulatoryActionId == selectedLesson._Regaction select c);
                    foreach (var c in res)
                    {
                        students += c.Group.StudCount;
                    }
                }
                if (students > selectedauditorium.Seats)
                {
                    small = true;
                    msg += "Аудитория мала для потока! ";
                }
                else msg += "Аудитория подходит. ";
                msg += "Студентов: " + students + " Мест: " + selectedauditorium.Seats;
            }
            else
            {
                //помещается ли группа?
                if (selectedGroup.StudCount > selectedauditorium.Seats)
                {
                    small = true;
                    msg += "Аудитория мала для этой группы! ";
                }
                else msg += "Аудитория подходит. ";
                msg += "Студентов: " + selectedGroup.StudCount + " Мест: " + selectedauditorium.Seats;
            }
            ErrorInfo = msg;

            if (!small && empty)
            {
                selectedLesson._AudID = selectedauditorium.Id;
                selectedLesson._Auditorium = "ауд. " + selectedauditorium.Number;
            }
        }
        #endregion
    }
}
