using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    class Day04
    {
        static void Main(string[] args)
        {
            //sort input by date and time
            Regex logRegex = new Regex(@"\[1518-(\d\d)-(\d\d) (\d\d):(\d\d)\] (.*)");
            MatchCollection logEntries = logRegex.Matches(System.IO.File.ReadAllText("input.txt"));
            LogEntry[] entries = new LogEntry[logEntries.Count];
            for (int i = 0; i < logEntries.Count; i++)
            {
                //parse to get relevant info
                entries[i] = new LogEntry(int.Parse(logEntries[i].Groups[1].Value), 
                    int.Parse(logEntries[i].Groups[2].Value), 
                    int.Parse(logEntries[i].Groups[3].Value), 
                    int.Parse(logEntries[i].Groups[4].Value), 
                    logEntries[i].Groups[5].Value);
            }
            Array.Sort(entries);
            //read the log
            int guardID=0, previousMinute=0;
            Dictionary<int, GuardInfo> guards = new Dictionary<int, GuardInfo>();
            GuardInfo current, sleepiest = new GuardInfo
                {
                id = 0,
                slept = 0,
                map = new int[60]
                 };
            //count minutes slept by each guard
            //and create a minute map to identify the most sleepy minute later
            foreach (LogEntry entry in entries)
            {
                if(entry.action.StartsWith('G')) //guard begins shift
                {
                    guardID = int.Parse(entry.action.Replace("Guard #", "").Split(" ")[0]);
                }
                else if(entry.action.StartsWith('f')) //guard falls asleep
                {
                    previousMinute = entry.minute;
                }
                else //guard wakes up
                {
                    if(guards.ContainsKey(guardID))
                    {
                        current = guards[guardID];
                    }
                    else
                    {
                        current = new GuardInfo
                        {
                            id = guardID,
                            slept = 0,
                            map = new int[60]
                        };
                        guards.Add(guardID, current);
                    }
                    current.slept += (entry.minute - previousMinute);
                    for (int i = previousMinute; i < entry.minute; i++)
                    {
                        current.map[i]++;
                    }
                    guards[guardID] = current;
                    if(current.slept>sleepiest.slept)
                    {
                        sleepiest = current;
                    }
                }
            }
            Console.WriteLine("Sleepiest guard is " + sleepiest.id);
            Console.WriteLine("Sleepiest minute is " + Array.IndexOf(sleepiest.map,sleepiest.map.Max()));
            Console.Read();
        }

        public struct LogEntry: IComparable
        {
            public int month;
            public int day;
            public int hour;
            public int minute;
            public String action;

            public LogEntry(int m,int d, int h, int min, String act)
            {
                month = m;
                day = d;
                hour = h;
                minute = min;
                action = act;
            }
            public int CompareTo(Object other)
            {
                LogEntry that = (LogEntry) other;
                int result = this.month.CompareTo(that.month);
                if(result == 0) result = this.day.CompareTo(that.day);
                if (result == 0) result = this.hour.CompareTo(that.hour);
                if (result == 0) result = this.minute.CompareTo(that.minute);
                return result;
            }
        }

        public struct GuardInfo
        {
            public int id;
            public int slept;
            public int[] map;
        }
    }
}
