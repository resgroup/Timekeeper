using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RES_Timekeeper.Data;
using System.IO;

namespace RES_Timekeeper
{
    public static class Summariser
    {
        private static ProjectList _projectCache;

        public static void Summarise()
        {
            _projectCache = ProjectList.Load(false);

            // Get start of the week (monday) a year or so ago
            DateTime yearAgo = DateTime.Now.AddYears(-1).Date;
            //DateTime yearAgo = DateTime.Now.AddDays(-14).Date;
            yearAgo = yearAgo.AddDays(8-(int)yearAgo.DayOfWeek);
            while (yearAgo < DateTime.Now)
            {
                try
                {
                    SummariseWeek(yearAgo);
                }
                catch {}
                yearAgo = yearAgo.AddDays(7);
            }          
        }
                
        private static void SummariseWeek(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(7);

            ItemList weeksItems = ItemList.Load(weekStart, weekEnd);

            if (weeksItems.Count > 0 && weeksItems.UnconfirmedCount == 0)
            {
                // Get list of all projects in the week, sorted by Code
                var projectIDs = (from i in weeksItems.Items select i.ProjectID).Distinct();
                List<Tuple<int, string, string>> projectIdCodeTitle = new List<Tuple<int, string, string>>();
                foreach (int id in projectIDs)
                {
                    if (id > 1) // Skip lunch
                    {
                        Project p = _projectCache.Projects.Where(proj => proj.ID == id).First();
                        projectIdCodeTitle.Add(new Tuple<int, string, string>(p.ID, p.Code, p.Title));
                    }
                }
                projectIdCodeTitle.Sort((a, b) => a.Item2.CompareTo(b.Item2));

                string output = "";
                List<Tuple<int, double>>[] projectHoursInDayAndWeek = new List<Tuple<int, double>>[8];
                for (int i = 0; i < 7; i++)
                {
                    projectHoursInDayAndWeek[i] = new List<Tuple<int, double>>();
                    DateTime dayStart = weekStart.AddDays(i);
                    DateTime dayEnd = dayStart.AddDays(1);
                    output = output + ",\"" + dayStart.ToString("ddd dd MMM") + "\"";
                    IEnumerable<Item> itemsInDay = weeksItems.Items.Where(item => item.StartTime >= dayStart && item.EndTime < dayEnd);
                    foreach (Tuple<int, string, string> projects in projectIdCodeTitle)
                    {
                        IEnumerable<Item> projectItemsInDay = itemsInDay.Where(item => item.ProjectID == projects.Item1);
                        projectHoursInDayAndWeek[i].Add( new Tuple<int,double>(projects.Item1, projectItemsInDay.Sum(item => (item.EndTime - item.StartTime).TotalHours)) );
                    }
                }
                output = output + "\n";

                double[] daySums = new double[7];
                foreach (var project in projectIdCodeTitle)
                {
                    double projectSum = 0;
                    output = output + "\"" + project.Item2 + " (" + project.Item3.Trim() + ")\"";
                    for (int i = 0; i < 7; i++)
                    {
                        double hours = 0.0;
                        Tuple<int, double> projectHoursInDay = projectHoursInDayAndWeek[i].Where(j => j.Item1 == project.Item1).FirstOrDefault();
                        if (projectHoursInDay != null) hours = projectHoursInDay.Item2;
                        output = output + "," + hours.ToString("0.00");
                        projectSum += hours;
                        daySums[i] += hours;
                    }
                    output = output + "," + projectSum.ToString("0.00");
                    output = output + "\n";
                }

                for (int i = 0; i < 7; i++)
                {
                    output = output + "," + daySums[i].ToString("0.00");
                }
                output = output + "\n\n\n";

                var weeksWork = weeksItems.Items.ToList<Item>();
                weeksWork.Sort();
                foreach (Item i in weeksWork)
                {
                    Project p = _projectCache.GetFromID(i.ProjectID);
                    output = output + string.Format(",,,,,,,,,,{0},{1},\"{2}\",\"{3}\",\"{4}\"\n", i.StartTime.ToString("yyyy-MMM-dd HH:mm:ss"), i.EndTime.ToString("yyyy-MMM-dd HH:mm:ss"), p.Code, p.Title, i.Notes);
                }

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = Path.Combine(folder, "RES-Timekeeper Reports");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string filename = Path.Combine(folder, "Week Commencing " + weekStart.ToString("yyyy-MM-dd") + ".csv");

                System.Diagnostics.Debug.WriteLine(output);
                using (StreamWriter s = new StreamWriter(filename))
                {
                    s.WriteLine(output);
                }                
            }
        }
    }
}
