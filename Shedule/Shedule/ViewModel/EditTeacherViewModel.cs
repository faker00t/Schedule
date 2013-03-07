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
    class EditTeacherViewModel : ViewModelBase
    {
        ObservableCollection<Employe> employes;
        public ObservableCollection<Employe> Employes
        {
            get { return employes; }
            set
            {
                employes = value;
                OnPropertyChanged("Employes");
            }
        }

        ObservableCollection<Faculty> faculties;
        public ObservableCollection<Faculty> Faculties
        {
            get { return faculties; }
            set
            {
                faculties = value;
                OnPropertyChanged("Faculties");
            }
        }

        ObservableCollection<Degree> degrees;
        public ObservableCollection<Degree> Degrees
        {
            get { return degrees; }
            set
            {
                degrees = value;
                OnPropertyChanged("Degrees");
            }
        }

        ObservableCollection<Title> titles;
        public ObservableCollection<Title> Titles
        {
            get { return titles; }
            set
            {
                titles = value;
                OnPropertyChanged("Titles");
            }
        }

        Employe selectedemploye;
        public Employe SelectedEmploye
        {
            get { return selectedemploye; }
            set
            {
                selectedemploye = value;
                OnPropertyChanged("SelectedEmploye");
            }
        }

        public EditTeacherViewModel()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var deg = (from d in cnt.Degrees select d);
                Degrees = new ObservableCollection<Degree>(deg);
                var fac = (from f in cnt.Faculties select f);
                Faculties = new ObservableCollection<Faculty>(fac);
                var titl = (from t in cnt.Titles select t);
                Titles = new ObservableCollection<Title>(titl);
                var emp = (from e in cnt.Employees select e);
                Employes = new ObservableCollection<Employe>(emp);
            }
        }

        #region добавить сотрудника
        private DelegateCommand addEmploye;

        public ICommand AddEmployeCommand
        {
            get
            {
                if (addEmploye == null)
                {
                    addEmploye = new DelegateCommand(AddEmploye);
                }
                return addEmploye;
            }
        }

        private void AddEmploye()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                Employe emp = new Employe();
                emp.Name = selectedemploye.Name;
                emp.DegreeId = selectedemploye.DegreeId;
                emp.FacultyId = selectedemploye.FacultyId;
                emp.TitleId = selectedemploye.TitleId;
                cnt.Employees.AddObject(emp);
                cnt.SaveChanges();
                var emplist = (from e in cnt.Employees select e);
                Employes = new ObservableCollection<Employe>(emplist);
            }
        }
        #endregion

        #region редактировать сотрудника
        private DelegateCommand editEmploye;

        public ICommand EditEmployeCommand
        {
            get
            {
                if (editEmploye == null)
                {
                    editEmploye = new DelegateCommand(EditEmploye);
                }
                return editEmploye;
            }
        }

        private void EditEmploye()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var emp = (from e in cnt.Employees where e.Id == selectedemploye.Id select e).First();
                emp.Name = selectedemploye.Name;
                emp.DegreeId = selectedemploye.DegreeId;
                emp.FacultyId = selectedemploye.FacultyId;
                emp.TitleId = selectedemploye.TitleId;
                //cnt.Refresh(System.Data.Objects.RefreshMode.ClientWins, emp);
                cnt.SaveChanges();
                var emplist = (from e in cnt.Employees select e);
                Employes = new ObservableCollection<Employe>(emplist);
            }
        }
        #endregion

        #region удалить сотрудника
        private DelegateCommand delEmploye;

        public ICommand DelEmployeCommand
        {
            get
            {
                if (delEmploye == null)
                {
                    delEmploye = new DelegateCommand(DelEmploye);
                }
                return delEmploye;
            }
        }

        private void DelEmploye()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var emp = (from e in cnt.Employees where e.Id == selectedemploye.Id select e).First();
                cnt.DeleteObject(emp);
                cnt.SaveChanges();
                var emplist = (from e in cnt.Employees select e);
                Employes = new ObservableCollection<Employe>(emplist);
            }
        }
        #endregion
    }
}
