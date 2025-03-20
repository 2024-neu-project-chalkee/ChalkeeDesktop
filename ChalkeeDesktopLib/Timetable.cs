using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkeeDesktopLib
{
    class Timetable(
        int day,
        int period,
        string subject,
        string name,
        string room,
        string @class,
        string group,
        string classroom,
        string grouproom,
        string status)
    {

        public int Day { get; init; } = day;
        public int Period { get; init; } = period;
        private string Subject { get; init; } = subject;
        private string Name { get; init; } = name;
        private string Room { get; init; } = room;
        private string? Class { get; init; } = @class;
        private string? Group { get; init; } = group;
        private string? Classroom { get; init; } = classroom;
        private string? Grouproom { get; init; } = grouproom;
        private string? Status { get; init; } = status;

        public override string ToString()
        {
            return $"{new[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}[Day]}:\n" +
                $"\tPeriod: {Period}\n" +
                $"\tSubject: {Subject}\n" +
                $"\tName of teacher: {Name}\n" +
                $"\tRoom: {Room}\n" +
                $"\tClass: {Class}\n" +
                $"\tGroup: {Group}\n" +
                $"\tClass room: {Classroom}\n" +
                $"\tGroup room: {Grouproom}\n" +
                $"\tStatus: {Status}\n";
        }

    }
}
