using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Shedule.Data;

namespace Shedule.Import
{
    public class ReadedString
    {
        public bool Add { get; set; }
        public int Control { get; set; }
        public string Date { get; set; }
        public string Groups { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public int Semestr { get; set; }
        public int Session { get; set; }
        public string Subject { get; set; }
        public int SubjectType { get; set; }
        public string SummerControl { get; set; }
        public int Time { get; set; }
        public string WinterControl { get; set; }
        public int Zaoch { get; set; }
        public int KafedraId { get; set; }
    }


    public enum ESession
    {
        Installation = 1,
        Winter = 2,
        Summer = 3,
        All = 4
    }
    /// выделить куда-то ^^^^^^^^^^^^^^^^^^

    public class StudyLoadReader
    {
        private StreamReader Sr { get; set; }

        public List<ReadedString> ReadFile(string filename)
        {
            var res = new List<ReadedString>();

            string kafedraId = filename.Substring(filename.LastIndexOf('\\') + 1).Remove(0, 4).Remove(1);
            Sr = new StreamReader(filename, Encoding.Default);

            while (!Sr.EndOfStream)
            {
                var readerString = new ReadedString();
                readerString.Date = "";
                readerString.Name = string.Empty;
                readerString.Subject = string.Empty;
                string line = Sr.ReadLine();
                if (!String.IsNullOrEmpty(line))
                {
                    readerString.KafedraId = Convert.ToInt32(kafedraId);
                    readerString.Zaoch = Convert.ToInt32(line.Substring(17, 1));
                    readerString.Name = line.Substring(22, 30).Trim();
                    if (readerString.Name != "")
                    {
                        readerString.Name2 = readerString.Name[0] == 'e' || readerString.Name[0] == 'i' || readerString.Name[0] == 'h'
                                                     ? readerString.Name.Remove(0, 1) : readerString.Name;
                        int n = readerString.Name2.Count(x => x == ' ');
                        if (n == 2)
                        {
                            readerString.Name2 = readerString.Name2.Remove(readerString.Name2.IndexOf(' '), 1);
                        }

                        readerString.Subject = line.Substring(52, 100).Trim();

                        string subject = line.Substring(152, 4).Trim();
                        switch (subject)
                        {
                            case "Лек":
                                {
                                    readerString.SubjectType = 1;
                                    break;
                                }
                            case "Пр":
                                {
                                    readerString.SubjectType = 2;
                                    break;
                                }

                            default:
                                {
                                    readerString.SubjectType = 3;
                                    break;
                                }
                        }

                        readerString.Groups = line.Substring(166, 101).Trim();

                        switch (line.Substring(290, 9).Trim())
                        {
                            case "Эк":
                                {
                                    readerString.Control = 1;
                                    break;
                                }
                            case "За":
                                {
                                    readerString.Control = 2;
                                    break;
                                }
                        }
                        readerString.WinterControl = line.Substring(290, 4).Trim();
                        readerString.SummerControl = line.Substring(294, 5).Trim();
                        string tm = line.Substring(331, 4).Trim();
                        if (tm != "")
                        {
                            readerString.Time = Convert.ToInt32(tm);
                        }

                        readerString.Semestr = Convert.ToInt32(line.Substring(305, 2).Trim());
                        readerString.Session = readerString.Semestr % 2 == 0 ? 3 : 2;
                        if (readerString.Zaoch == 1)
                        {
                            string ust = line.Substring(337, 5).Trim();
                            string zim = line.Substring(343, 5).Trim();
                            string let = line.Substring(349, 5).Trim();
                            string[] mas = {
                                                   ust, zim, let
                                           };
                            for (int i = 0; i < mas.Length; i++)
                            {
                                if (mas[i] != "0")
                                {
                                    readerString.Time = Convert.ToInt32(mas[i]);
                                    readerString.Session = i + 1;
                                }
                            }
                            switch ((ESession)readerString.Session)
                            {
                                case ESession.Installation:
                                    readerString.Date = line.Substring(354, 20);
                                    break;
                                case ESession.Winter:
                                    readerString.Date = line.Substring(374, 20);
                                    break;
                                case ESession.Summer:
                                    readerString.Date = line.Substring(394, 20);
                                    break;
                            }
                        }
                        else
                        {
                            readerString.Date = line.Substring(354, 20);
                        }
                    }


                    res.Add(readerString);
                }
            }
            return res;
        }
    }
}
