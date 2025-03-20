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
        string? @class,
        string? group,
        string? classroom,
        string? grouproom,
        string? status)
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
            return $"\tPeriod: {Period}\n" +
                $"\tSubject: {Subject}\n" +
                $"\tName of teacher: {Name}\n" +
                $"\tRoom: {Room}\n" +
                $"\t{(Class != null ? $"Class: {Class}" : $"Group: {Group}")}\n" +
                $"\t{(Classroom != null ? $"Class room: {Classroom}" : $"Group room: {Grouproom}")}\n" +
                $"{(Status != null ? $"\tStatus: {Status}\n" : "")}";
        }

    }
}
