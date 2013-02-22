using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using Schedule.BL;
//using Schedule.Common;

using Shedule.Data;

namespace Shedule.Import
{
    public class ReadedGroup
    {
        public bool Add { get; set; }
        public string groupCode { get; set; }
        public string facultet { get; set; }
        public int course { get; set; }
        public string specCode { get; set; }
        public int studentAmount { get; set; }
        public int studyDirection { get; set; }
        public int studyForm { get; set; }

        public ReadedGroup(string _groupCode, string _facultet, int _course, string _specCode, int _studentAmount, int _studyDirection, int _studyForm)
        {
            groupCode = _groupCode;
            facultet = _facultet;
            course = _course;
            specCode = _specCode;
            studentAmount = _studentAmount;
            studyDirection = _studyDirection;
            studyForm = _studyForm;
        }
    }

    public class GroupReader
    {
        public readonly List<ReadedGroup> GroupList;

        public GroupReader()
        {
            GroupList = new List<ReadedGroup>();
        }

        public List<ReadedGroup> ReadFile(string fileName)
        {
            StreamReader _sr = new StreamReader(fileName, Encoding.Default);
            int studyDirection = 1;
            int studyForm = 2;

            while (!_sr.EndOfStream)
            {
                string line = _sr.ReadLine();
                if (line == null)
                {
                    continue;
                }

                string facultet = line.Substring(11, 8).Trim();
                string specCode = line.Substring(25, 24).Trim();
                if (specCode[specCode.Length - 1] != 'A')
                {
                    string groupCode = line.Substring(0, 11).Trim();
                    int course = Convert.ToInt32(line.Substring(19, 1).Trim());
                    int studentAmount = Convert.ToInt32(line.Substring(22, 2).Trim());

                    if (specCode[specCode.Length - 1] == 'I')
                    {
                        studyDirection = 1;
                        studyForm = specCode[0] == 'S' ? 2 : 1;
                    }
                    else
                    {
                        if (specCode[specCode.Length - 1] == 'Z')
                        {
                            studyForm = specCode[0] == 'S' ? 4 : 3;
                            studyDirection = 2;
                        }
                        else if (course > 4)
                        {
                            studyDirection = 3;
                            studyForm = specCode[0] == 'S' ? 2 : 1;
                        }
                        else if ((course > 0) & (course < 5))
                        {
                            studyDirection = 2;
                        }
                    }

                    specCode = specCode.Substring(0, specCode.LastIndexOf('-')); //-

                    //object[] obj = new object[]
                    //                   {
                    GroupList.Add(new ReadedGroup(groupCode, facultet, course, specCode, studentAmount, studyDirection, studyForm));
                    //GroupList.Add(new Group()
                    //{
                    //    GroupAbbreviation = tokens[0],
                    //    Cource = tokens[1],
                    //    StudCount = tokens[3],
                    //    SpecialtyAbbreviation = tokens[4],
                    //    FacultyId = fac.Id,
                    //    StudyTypeId = stdtype.Id,
                    //    FieldOfStudyId = stdfield.Id,
                    //    EduPeriodId = year.Id
                    //});
                    //        };
                 }
            }
            return GroupList;
        }
    }
}
