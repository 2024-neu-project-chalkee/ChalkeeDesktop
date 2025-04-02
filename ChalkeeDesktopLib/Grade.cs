using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalkeeDesktopLib
{
    internal class Grade
    {

       
        public int GradeVal { get; init; }
        public int GradeWeight { get; init; }
        public string AnnouncementType{ get; init; }
        public string Content{ get; init; }
        public DateOnly DateOfGrade { get; init; }
        public string Name{ get; init; }
        public string SubjectName { get; init; }
        public string GroupName { get; init; }
        public string ClassName { get; init; }

        public Grade( 
            int gradeVal, 
            int gradeWeight, 
            string announcementType, 
            string content, 
            DateOnly dateOfGrade, 
            string name, 
            string subjectName, 
            string groupName, 
            string className)
        {
            
            GradeVal = gradeVal;
            GradeWeight = gradeWeight;
            AnnouncementType = announcementType;
            Content = content;
            DateOfGrade = dateOfGrade;
            Name = name;
            SubjectName = subjectName;
            GroupName = groupName;
            ClassName = className;
        }




    }
}
