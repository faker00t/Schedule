using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shedule.Data;

namespace Shedule.Common
{
    class DisplayClasses
    {
    }

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
        public string _Group { get; set; }
        public string _Teacher { get; set; }
        public string _Type { get; set; }
        public int _Day { get; set; }
        public int _Number { get; set; }
        public int _Ring { get; set; }
        //public Ring _RingObj { get; set; }
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
            copy._Group = _Group;
            copy._Teacher = _Teacher;
            copy._Type = _Type;
            copy._Day = _Day;
            copy._Number = _Number;
            copy._Ring = _Ring;
            //copy._RingObj = _RingObj;
            copy._Hours = _Hours;
            copy._Error = _Error;
            copy._LessonID = _LessonID;
            copy._Flow = _Flow;
            copy._Auditorium = _Auditorium;
            copy._Date = _Date;
            return copy;
        }
    }
}
