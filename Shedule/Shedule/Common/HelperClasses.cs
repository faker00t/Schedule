using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shedule.Common
{
    class HelperClasses
    {
        public static DateTime WeekDayNumberToDay(int daynum, DateTime selecteddate) // преобразует день недели от 0 до 6 (пн-вс) в дату, на основе выбранной в датапикере даты
        {
            int selday = (int)selecteddate.DayOfWeek;
            return selecteddate.AddDays(daynum + 1 - selday);
        }

        public static void indexToNumberDay(int i, out int number, out int day)
        {
            number = (int)(i / 7) + 1;
            day = (i % 7);
        }

        public static int numberDayToIndex(int day, int number)
        {
            return day + (number - 1) * 7;
        }

        public static bool IsUpweek(DateTime selecteddate)
        {
            return true;
        }
    }

    public enum StudyForm
    {
        Och = 1,
        Zaoch = 2,
        OchShort = 3,
        ZaochShost = 4,
    }
}
