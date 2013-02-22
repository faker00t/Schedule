using System.Collections.Generic;
using System.IO;
using System.Text;

using Shedule.Data;

namespace Shedule.Import
{
    public class ReadedDepartment
    {
        public bool Add { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class DepartmentReader
    {
        List<ReadedDepartment> ChairList;

        public DepartmentReader()
        {
            ChairList = new List<ReadedDepartment>();
        }

        public List<ReadedDepartment> ReadFile(string fileName)
        {
            int id = 1;
            StreamReader _sr = new StreamReader(fileName, Encoding.Default);
            while (!_sr.EndOfStream)
            {
                //var scheduleModule = new ScheduleDbModule("scheduleDb");
                string line = _sr.ReadLine();
                if (line != null)
                {
                    string fullName = line.Substring(0, line.Length).Trim();
                    var chairData = new ReadedDepartment();
                    if (fullName != null)
                    {
                        chairData.Name = fullName;
                        //chairData.Id = id;
                        ++id;
                    }
                    ChairList.Add(chairData);
                }
            }
            return ChairList;
        }
    }
}
