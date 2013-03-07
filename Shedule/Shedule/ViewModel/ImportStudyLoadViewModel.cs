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
using Shedule.Common.Extensions;
using System.Windows.Threading;

namespace Shedule.ViewModel
{
    class ImportStudyLoadViewModel : ViewModelBase
    {
        #region doevents
        public void DoEvents() // обработка событий во время длительных операций
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        public object ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }
        #endregion

        #region Чекбоксы
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

        private bool winter; // зимняя сессия 
        public bool Winter
        {
            get { return winter; }
            set
            {
                winter = value;
                OnPropertyChanged("Winter");
            }
        }

        private bool summer; // летняя сессия 
        public bool Summer
        {
            get { return summer; }
            set
            {
                summer = value;
                OnPropertyChanged("Summer");
            }
        }

        private bool installation; // установочная сессия 
        public bool Installation
        {
            get { return installation; }
            set
            {
                installation = value;
                OnPropertyChanged("Installation");
            }
        }

        private bool selectall; // установочная сессия 
        public bool SelectAll
        {
            get { return selectall; }
            set
            {
                selectall = value;
                OnPropertyChanged("SelectAll");
            }
        }
        #endregion

        private string fromField; // заменять с
        public string FromField
        {
            get { return fromField; }
            set
            {
                fromField = value;
                OnPropertyChanged("FromField");
            }
        }

        private string toField; // заменять по
        public string ToField
        {
            get { return toField; }
            set
            {
                toField = value;
                OnPropertyChanged("ToField");
            }
        }

        #region Конструктор
        public ImportStudyLoadViewModel()
        {
            Och = true;
            ZaOch = true;
            Winter = true;
            Summer = true;
            Installation = true;
            SelectAll = true;

            Curriculums = new ObservableCollection<Curriculum>();

            FromField = string.Empty;
            ToField = string.Empty;
        }
        #endregion

        private string inputfilename; // имя входного файла 
        public string InputFileName
        {
            get { return inputfilename; }
            set
            {
                inputfilename = value;
                OnPropertyChanged("InputFileName");
            }
        }

        private string message; // сообщение на форме
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        ObservableCollection<Curriculum> curriculums;
        public ObservableCollection<Curriculum> Curriculums
        {
            get { return curriculums; }
            set
            {
                curriculums = value;
                OnPropertyChanged("Curriculums");
            }
        }

        ObservableCollection<ReadedString> readedstrings;
        public ObservableCollection<ReadedString> ReadedStrings
        {
            get { return readedstrings; }
            set
            {
                readedstrings = value;
                OnPropertyChanged("ReadedStrings");
            }
        }

        #region Применить
        private DelegateCommand applay;

        public ICommand ApplayCommand
        {
            get
            {
                if (applay == null)
                {
                    applay = new DelegateCommand(Applay);
                }
                return applay;
            }
        }
        public void Applay()
        {
            StudyLoadReader reader = new StudyLoadReader();
            List<ReadedString> result = reader.ReadFile(InputFileName);

            List<ReadedString> filtered = new List<ReadedString>();
            foreach (var res in result)
            {
                if (selectall) res.Add = true;
                if (res.Date.Length > 10 && DateTime.Parse(res.Date.Substring(10, 10)) > DateTime.Today) res.Actual = "Актуально";
                else res.Actual = "Не актуально";
                if ((res.Zaoch == 1) && zaoch)
                {
                    if ((res.Session == 1) && installation)
                    {
                        filtered.Add(res);
                    }
                    else if ((res.Session == 2) && winter)
                    {
                        filtered.Add(res);
                    }
                    else if ((res.Session == 3) && summer)
                    {
                        filtered.Add(res);
                    }
                }

                if ((res.Zaoch == 0) && och)
                {
                    if ((res.Session == 1) && installation)
                    {
                        filtered.Add(res);
                    }
                    else if ((res.Session == 2) && winter)
                    {
                        filtered.Add(res);
                    }
                    else if ((res.Session == 3) && summer)
                    {
                        filtered.Add(res);
                    }
                }
            }
            ReadedStrings = new ObservableCollection<ReadedString>(filtered);
        }
        #endregion

        #region Импортировать
        private DelegateCommand import;

        public ICommand ImportCommand
        {
            get
            {
                if (import == null)
                {
                    import = new DelegateCommand(Import);
                }
                return import;
            }
        }
        public void Import()
        {
            int lines_count = readedstrings.Where(x => x.Add).Count();
            int i = 0;
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                int depId = ReadedStrings.First().KafedraId;
                //Department department = (from lt in cnt.Departments where lt.Id == depId select lt).First();
                foreach (var res in ReadedStrings.Where(x => x.Add))
                {
                    if (res.SubjectType == 0) continue;
                    //LessonsType lessontype = (from lt in cnt.LessonsTypes where lt.Id == res.SubjectType select lt).First();
                    RegulatoryAction regaction = new RegulatoryAction()
                    {
                        LessonsTypeId = res.SubjectType,
                        Hours = res.Time,
                        DepartmentId = depId,
                    };
                    cnt.RegulatoryActions.AddObject(regaction);

                    IEnumerable<Employe> teachers = (from e in cnt.Employees where e.Name == res.Name2 select e);
                    Employe teacher = null;
                    if (teachers.Count() == 0)
                    {
                        //нет такого преподавателя, добавим его
                        teacher = new Employe()
                        {
                            Name = res.Name2,
                            FacultyId = 1,
                            TitleId = 1,
                            DegreeId = 1,
                        };
                        cnt.Employees.AddObject(teacher);
                        //cnt.SaveChanges();
                    }
                    else teacher = teachers.First();

                    AcademicLoad academicload = new AcademicLoad()
                    {
                        RegulatoryAction = regaction,
                        Employe = teacher
                    };
                    cnt.AcademicLoadSet.AddObject(academicload);

                    ///
                    IEnumerable<Subject> subjects = (from e in cnt.Subjects where e.Name == res.Subject select e);
                    Subject subject = null;
                    if (subjects.Count() == 0)
                    {
                        //нет такого предмета, добавим его
                        subject = new Subject()
                        {
                            Name = res.Subject,
                            Abbreviation = "",
                        };
                        cnt.Subjects.AddObject(subject);
                        Console.WriteLine("subj not found = " + res.Subject);
                    }
                    else
                    {
                        subject = subjects.First();
                        Console.WriteLine("subj found = " + subject.Name);
                    }
                    ///

                    ///
                    string [] splitedGroups = res.Groups.Split(';');
                    foreach (var splitedGroup in splitedGroups)
                    {
                        string trimedGroup = splitedGroup.Trim();
                        Console.WriteLine(res.Groups + "       " + trimedGroup);
                        IEnumerable<Group> groups = (from e in cnt.Groups.Include("EduPeriod") where e.GroupAbbreviation == trimedGroup select e);
                        Group group = null;
                        if (groups.Count() == 0)
                        {
                            Console.WriteLine("group not found = " + trimedGroup);
                            continue;
                        }
                        else
                        {
                            group = groups.First();
                            Console.WriteLine("group found = " + trimedGroup);
                            if (group.EduPeriod.Count == 0)
                            {
                                EduPeriod e = new EduPeriod
                                {
                                    Begin = DateTime.Parse(res.Date.Substring(0, 10)),
                                    End = DateTime.Parse(res.Date.Substring(10, 10)),
                                    GroupId = group.Id,
                                };
                                cnt.EduPeriods.AddObject(e);
                            }
                        }
                        ///

                        Curriculum curr = new Curriculum()
                        {
                            RegulatoryAction = regaction,
                            Subject = subject,
                            Group = group,
                        };
                        cnt.Curriculums.AddObject(curr);
                        cnt.SaveChanges();
                    }
                    ++i;
                    Message = "Выполняется импорт нагрузки. Добавлено " + i + " из " + lines_count;
                    DoEvents();
                }
                //cnt.SaveChanges();
            }
        }
        #endregion

        #region Выбор файла
        private DelegateCommand fileselect;

        public ICommand FileSelectCommand
        {
            get
            {
                if (fileselect == null)
                {
                    fileselect = new DelegateCommand(FileSelect);
                }
                return fileselect;
            }
        }
        public void FileSelect()
        {
            InputFileName = Reader.OpenFile();
            Applay();
        }
        #endregion

        #region смена дат
        private DelegateCommand dateChange;

        public ICommand DateChangeCommand
        {
            get
            {
                if (dateChange == null)
                {
                    dateChange = new DelegateCommand(DateChange);
                }
                return dateChange;
            }
        }
        public void DateChange()
        {
            foreach (var i in readedstrings)
            {
                if (fromField.Length == 4)
                {
                    i.Date = i.Date.Remove(6, 4).Insert(6, fromField);
                }

                if (toField.Length == 4)
                {
                    i.Date = i.Date.Remove(16, 4).Insert(16, toField);
                }
            }
            var tmp = new ObservableCollection<ReadedString>(readedstrings);
            ReadedStrings = tmp;
        }
        #endregion
    }
}
