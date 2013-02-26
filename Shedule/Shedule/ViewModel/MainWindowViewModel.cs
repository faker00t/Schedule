using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;

using Shedule.Data;
using Shedule.Commands;
using Shedule.Import;

//----------------------
using Shedule.ViewModel;
using Shedule.View;

namespace Shedule.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        #region GroupImport
        private DelegateCommand groupImport;

        public ICommand GroupImportCommand
        {
            get
            {
                if (groupImport == null)
                {
                    groupImport = new DelegateCommand(GroupImport);
                }
                return groupImport;
            }
        }
            
        private void GroupImport()
        {
            ImportGroupsForm IGF = new ImportGroupsForm();
            IGF.Show();
        }
        #endregion

        #region SheduleExport
        private DelegateCommand sheduleExport;

        public ICommand SheduleExportCommand
        {
            get
            {
                if (sheduleExport == null)
                {
                    sheduleExport = new DelegateCommand(SheduleExport);
                }
                return sheduleExport;
            }
        }

        private void SheduleExport()
        {
            //System.Windows.MessageBox.Show("SheduleExport");


            //using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            //{
            //    //once();
            //    var check = from u in cnt.Faculties where u.Name == "pushkin" select u;
            //    Console.WriteLine(check.Count());
            //    foreach (var user in check)
            //    {
            //        Console.WriteLine(user.Name + " = ");
            //    }
            //}


            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.StartInfo.FileName = "notepad.exe";
            //proc.StartInfo.WorkingDirectory = path + "\\MA7\\";
            //proc.Start();
            //proc.WaitForExit();
        }
        #endregion

        #region InstallApp
        private DelegateCommand installApp;

        public ICommand InstallAppCommand
        {
            get
            {
                if (installApp == null)
                {
                    installApp = new DelegateCommand(InstallApp);
                }
                return installApp;
            }
        }

        private void InstallApp()
        {
            StudyType Ochnaja = new StudyType
            {
                Name = "очная"
            };

            StudyType Zaochnaja = new StudyType
            {
                Name = "заочная"
            };

            StudyType unnamed3 = new StudyType
            {
                Name = "unnamed3"
            };

            StudyType unnamed4 = new StudyType
            {
                Name = "unnamed4"
            };

            Faculty IiVT = new Faculty
            {
                Name = "Информатика и вычислительная техника",
                Abbreviation = "ИиВТ",
            };

            FieldOfStudy Specialist = new FieldOfStudy
            {
                Name = "специалист"
            };

            FieldOfStudy Bakalavr = new FieldOfStudy
            {
                Name = "бакалавр"
            };

            FieldOfStudy Magistr = new FieldOfStudy
            {
                Name = "магистр"
            };

            LessonsType Lections = new LessonsType
            {
                Id = 1,
                Name = "лекция"
            };

            LessonsType Practics = new LessonsType
            {
                Id = 2,
                Name = "практика"
            };

            LessonsType Labs = new LessonsType
            {
                Id = 3,
                Name = "лабораторные"
            };

            LessonsSubType Comp = new LessonsSubType
            {
                Name = "компьютеры",
                LessonsTypeId = Labs.Id
            };

            LessonsSubType Stanki = new LessonsSubType
            {
                Name = "станки",
                LessonsTypeId = Labs.Id
            };

            Title prepod = new Title
            {
                Name = "преподаватель"
            };

            Faculty fac = new Faculty
            {
                Name = "факультет",
                Abbreviation = "фак"
            };

            Ring first = new Ring
            {
                Begin = "8:30",
                End = "9:50"
            };

            Ring second = new Ring
            {
                Begin = "10:00",
                End = "11:20"
            };

            Ring third = new Ring
            {
                Begin = "11:20",
                End = "12:50"
            };

            Ring fourth = new Ring
            {
                Begin = "13:30",
                End = "14:50"
            };

            Ring fifth = new Ring
            {
                Begin = "15:00",
                End = "16:20"
            };

            Ring sixth = new Ring
            {
                Begin = "16:30",
                End = "17:50"
            };

            Ring seventh = new Ring
            {
                Begin = "18:10",
                End = "19:30"
            };

            Auditorium a = new Auditorium
            {
                Building = 1,
                Number = "1-490",
                Seats = 50,
                OpeningDate = "",
                ClosingDate = "",
                DepartmentId = 1,
            };

            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                cnt.StudyTypes.AddObject(Ochnaja);
                cnt.StudyTypes.AddObject(Zaochnaja);
                cnt.StudyTypes.AddObject(unnamed3);
                cnt.StudyTypes.AddObject(unnamed4);
                cnt.Faculties.AddObject(IiVT);
                cnt.FieldsOfStudy.AddObject(Specialist);
                cnt.FieldsOfStudy.AddObject(Bakalavr);
                cnt.FieldsOfStudy.AddObject(Magistr);
                cnt.LessonsTypes.AddObject(Lections);
                cnt.LessonsTypes.AddObject(Practics);
                cnt.LessonsTypes.AddObject(Labs);
                cnt.LessonsSubTypes.AddObject(Comp);
                cnt.LessonsSubTypes.AddObject(Stanki);
                cnt.Titles.AddObject(prepod);
                cnt.Faculties.AddObject(fac);
                cnt.Rings.AddObject(first);
                cnt.Rings.AddObject(second);
                cnt.Rings.AddObject(third);
                cnt.Rings.AddObject(fourth);
                cnt.Rings.AddObject(fifth);
                cnt.Rings.AddObject(sixth);
                cnt.Rings.AddObject(seventh);
                //cnt.Auditoriums.AddObject(a);
                // И финальный аккорд - сохраняем все изменения в БД  
                cnt.SaveChanges();
            }

        }

        #endregion

        #region DepartmentImport
        private DelegateCommand departmentImport;

        public ICommand DepartmentImportCommand
        {
            get
            {
                if (departmentImport == null)
                {
                    departmentImport = new DelegateCommand(DepartmentImport);
                }
                return departmentImport;
            }
        }

        private void DepartmentImport()
        {
            ImportDepartmentsForm IDF = new ImportDepartmentsForm();
            IDF.Show();
        }
        #endregion

        #region StudyImport
        private DelegateCommand studyImport;

        public ICommand StudyImportCommand
        {
            get
            {
                if (studyImport == null)
                {
                    studyImport = new DelegateCommand(StudyImport);
                }
                return studyImport;
            }
        }

        private void StudyImport()
        {
            ImportStudyLoadForm ISLF = new ImportStudyLoadForm();
            ISLF.Show();
        }
        #endregion

        #region GroupImport
        private DelegateCommand auditoriumImport;

        public ICommand AuditoriumImportCommand
        {
            get
            {
                if (auditoriumImport == null)
                {
                    auditoriumImport = new DelegateCommand(AuditoriumImport);
                }
                return auditoriumImport;
            }
        }

        private void AuditoriumImport()
        {
            ImportAuditoriumsForm IAF = new ImportAuditoriumsForm();
            IAF.Show();
        }
        #endregion

        #region расписание преподавателя
        private DelegateCommand sheduleTeacher;

        public ICommand SheduleTeacherCommand
        {
            get
            {
                if (sheduleTeacher == null)
                {
                    sheduleTeacher = new DelegateCommand(SheduleTeacher);
                }
                return sheduleTeacher;
            }
        }

        private void SheduleTeacher()
        {
            ShedTeacherForm STF = new ShedTeacherForm();
            STF.Show();
        }
        #endregion

        #region расписание аудиторий
        private DelegateCommand sheduleAuditorium;

        public ICommand SheduleAuditoriumCommand
        {
            get
            {
                if (sheduleAuditorium == null)
                {
                    sheduleAuditorium = new DelegateCommand(SheduleAuditorium);
                }
                return sheduleAuditorium;
            }
        }

        private void SheduleAuditorium()
        {
            ShedAuditoriumForm SAF = new ShedAuditoriumForm();
            SAF.Show();
        }
        #endregion

        #region PrepairTestData
        private DelegateCommand prepairTestData;

        public ICommand PrepairTestDataCommand
        {
            get
            {
                if (prepairTestData == null)
                {
                    prepairTestData = new DelegateCommand(PrepairTestData);
                }
                return prepairTestData;
            }
        }

        void PrepairTestData()
        {
            using (UniversitySheduleContainer cnt = new UniversitySheduleContainer("name=UniversitySheduleContainer"))
            {
                var aud = (from a in cnt.Auditoriums select a);
                StreamWriter outfile = new StreamWriter("dstu.cfg", false);
                var prof = (from e in cnt.Employees select e);
                foreach (var p in prof)
                {
                    outfile.WriteLine();
                    outfile.WriteLine("#prof");
                    outfile.WriteLine("	id = " + p.Id);
                    //outfile.WriteLine("	name = " + p.Name);
                    outfile.WriteLine("	name = " + p.Id);
                    outfile.WriteLine("#end");
                }
                var cource = (from s in cnt.Subjects select s);
                foreach (var c in cource)
                {
                    outfile.WriteLine();
                    outfile.WriteLine("#course");
                    outfile.WriteLine("	id = " + c.Id);
                    //outfile.WriteLine("	name = " + c.Name);
                    outfile.WriteLine("	name = " + c.Id);
                    outfile.WriteLine("#end");
                }
                foreach (var a in aud)
                {
                    outfile.WriteLine();
                    outfile.WriteLine("#room");
                    //outfile.WriteLine("	name = " + a.Number);
                    outfile.WriteLine("	name = a" + a.Id);
                    outfile.WriteLine("	lab = false");
                    outfile.WriteLine("	size = " + a.Seats);
                    outfile.WriteLine("#end");
                }
                var group = (from g in cnt.Groups select g);
                foreach (var g in group)
                {
                    outfile.WriteLine();
                    outfile.WriteLine("#group");
                    outfile.WriteLine("	id = " + g.Id);
                    //outfile.WriteLine("	name = " + g.GroupAbbreviation);
                    outfile.WriteLine("	name = " + g.Id);
                    outfile.WriteLine("	size = " + g.StudCount);
                    outfile.WriteLine("#end");
                }
                var clas = (from c in cnt.Curriculums.Include("RegulatoryAction") select c);
                foreach (var c in clas)
                {
                    outfile.WriteLine();
                    outfile.WriteLine("#class");
                    outfile.WriteLine("	professor = " + c.RegulatoryAction.AcademicLoad.First().EmployeId);
                    outfile.WriteLine("	course = " + c.SubjectId);
                    outfile.WriteLine("	duration = 1");
                    outfile.WriteLine("	group = " + c.GroupId);
                    //outfile.WriteLine("	lab = false");
                    outfile.WriteLine("#end");
                }
                outfile.Close();
            }
        }
        #endregion
    }
}
