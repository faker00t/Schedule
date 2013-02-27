using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Shedule.Data;

namespace Shedule.Import
{
    public class ReadedAuditorium
    {
        public bool Add { get; set; }
            public int Corpus { get; set; }
            public string CorAndNum { get; set; }
            public int Capacity { get; set; }
            public int Chair { get; set; }
    }

    public class AuditReader
    {
        List<ReadedAuditorium> AuditList;

        public AuditReader()
        {
            AuditList = new List<ReadedAuditorium>();
        }

        public List<ReadedAuditorium> ReadFile(string fileName)
        {
            var _sr = new StreamReader(fileName, Encoding.GetEncoding("windows-1251"));
            int count = 0;
            _sr.ReadLine();
            while (!_sr.EndOfStream)
            {
                //var scheduleModule = new ScheduleDbModule("scheduleDb");
                string line = _sr.ReadLine();
                //int h = 0;
                if (line != null)
                {
                    string pr = line.Substring(22, line.Length - 22).Trim();
                    if (pr != '+'.ToString(CultureInfo.InvariantCulture))
                    {
                        int chair = Convert.ToInt32(line.Substring(14, 3).Trim());
                        
                        int capacity = Convert.ToInt32(line.Substring(17, 4).Trim());
                        string corAndNum = line.Substring(22, line.Length - 22).Trim();
                        int corpus;
                        string zr;
                        if (corAndNum.Contains('-') && !corAndNum.Contains('+'))
                        {
                            zr = corAndNum[0].ToString(CultureInfo.InvariantCulture);
                            corpus = Convert.ToInt32(zr);
                        }
                        else
                        {
                            corpus = 0;
                        }
                        if (corAndNum.Contains('-') && corAndNum.Contains('+'))
                        {
                            zr = corAndNum[1].ToString(CultureInfo.InvariantCulture);
                            corpus = Convert.ToInt32(zr);
                        }

                        //var auditData = new Auditorium(corpus, corAndNum, capacity, chair);
                        var auditData = new ReadedAuditorium()
                        {
                            Corpus = corpus,
                            Capacity = capacity,
                            Chair = chair,
                            CorAndNum = corAndNum,
                        };
                       // _auditDatas = scheduleModule.Audit_GetAll();
                        //foreach (Auditorium auditData1 in _auditDatas)
                        //{
                            //if (auditData1.CorAndNum == corAndNum)
                            //{
                            //    h = 1;
                            //}
                        //}
                        //if (h == 0)
                        //{
                            AuditList.Add(auditData);
                            count = count + 1;
                        //}
                    }
                }
            }

            return AuditList;

            //MessageBox.Show(@"Количество импортированных аудиторий = " + count);
        }
    }
}
