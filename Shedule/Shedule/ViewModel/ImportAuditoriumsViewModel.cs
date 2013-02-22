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

namespace Shedule.ViewModel
{
    class ImportAuditoriumsViewModel : ViewModelBase
    {
        private bool selectall; // 
        public bool SelectAll
        {
            get { return selectall; }
            set
            {
                selectall = value;
                OnPropertyChanged("SelectAll");
            }
        }

        ObservableCollection<ReadedAuditorium> readedauditoriums;
        public ObservableCollection<ReadedAuditorium> ReadedAuditoriums
        {
            get { return readedauditoriums; }
            set
            {
                readedauditoriums = value;
                OnPropertyChanged("ReadedAuditoriums");
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
        public ImportAuditoriumsViewModel()
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
                foreach (var g in readedauditoriums)
                {
                    if (g.Add)
                    {
                        var deps = (from lt in cnt.Departments where lt.Id == g.Chair select lt);
                        if (deps.Count() == 0) continue;


                        Auditorium newAudit = new Auditorium()
                        {
                            Building = g.Corpus.ToString(),
                            Department  = deps.First(),
                            Seats = g.Capacity.ToString(),
                             Number = g.CorAndNum,
                             OpeningDate = "",
                             ClosingDate = "",
                        };
                        cnt.Auditoriums.AddObject(newAudit);
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
            AuditReader reader = new AuditReader();
            List<ReadedAuditorium> result = reader.ReadFile(InputFileName);
            if (selectall)
            {
                foreach (var r in result)
                {
                    r.Add = true;
                }
            }
            ReadedAuditoriums = new ObservableCollection<ReadedAuditorium>(result);
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
