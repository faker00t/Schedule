using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;

using System.Collections.ObjectModel;
using Shedule.Import;
using Shedule.Data;
using Shedule.Commands;

namespace Shedule.ViewModel
{
    class ImportGroupsViewModel : ViewModelBase
    {
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

        ObservableCollection<ReadedGroup> readedgroups;
        public ObservableCollection<ReadedGroup> ReadedGroups
        {
            get { return readedgroups; }
            set
            {
                readedgroups = value;
                OnPropertyChanged("ReadedGroups");
            }
        }

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

        #region Конструктор
        public ImportGroupsViewModel()
        {
            SelectAll = true;
        }
        #endregion

        #region Import
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
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                foreach (var g in readedgroups)
                {
                    if (g.Add)
                    {
                        Group newGroup = new Group()
                        {
                            GroupAbbreviation = g.groupCode,
                            Cource = g.course,
                            StudCount = g.studentAmount,
                            SpecialtyAbbreviation = g.specCode,
                            StudyTypeId = g.studyForm,
                            FieldOfStudyId = g.studyDirection,
                            FacultyId = 1,
                            EduPeriodId = 1,
                        };
                        cnt.Groups.AddObject(newGroup);
                    }
                }
                cnt.SaveChanges();
            }
        }
        #endregion

        #region Applay
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
            GroupReader reader = new GroupReader();
            List<ReadedGroup> result = reader.ReadFile(InputFileName);
            if (selectall)
            {
                foreach (var r in result)
                {
                    r.Add = true;
                }
            }
            ReadedGroups = new ObservableCollection<ReadedGroup>(result);
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
        }
        #endregion
    }
}
