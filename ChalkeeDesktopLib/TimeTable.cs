using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkeeDesktopLib
{
    class TimeTable
    {

        public int Day { get; init; }
        public int Period { get; init; }
        private string Subject { get; init; }
        private string Name { get; init; }
        private string Room { get; init; }
        private string Class { get; init; }
        private string Group{ get; init; }
        private string ClassRoom { get; init; }
        private string GroupRoom{ get; init; }
        private string Status{ get; init; }

        public TimeTable(int day, int period, string subject, string name, string room, string @class, string group, string classRoom, string groupRoom, string status)
        {
            Day = day;
            Period = period;
            Subject = subject;
            Name = name;
            Room = room;
            Class = @class;
            Group = group;
            ClassRoom = classRoom;
            GroupRoom = groupRoom;
            Status = status;
        }

        public override string ToString()
        {
            return $"{Day}. nap:\n" +
                $"\tPeriod: {Period}\n" +
                $"\tSubject: {Subject}\n" +
                $"\tName of teacher: {Name}\n" +
                $"\tRoom: {Room}\n" +
                $"\tClass: {Class}\n" +
                $"\tGroup: {Group}\n" +
                $"\tClass room: {ClassRoom}\n" +
                $"\tGroup room: {GroupRoom}\n" +
                $"\tStatus: {Status}\n";
        }

    }
}
