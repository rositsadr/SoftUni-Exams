using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    public class Classroom
    {
        public List<Student> studentsInClass;

        public int Capacity { get; set; }

        public Classroom(int capacity)
        {
            studentsInClass = new List<Student>();
            this.Capacity = capacity;
        }

        public int Count => studentsInClass.Count;

        public string RegisterStudent(Student student)
        {
            if (studentsInClass.Count<Capacity)
            {
                studentsInClass.Add(student);
                return $"Added student {student.FirstName} {student.LastName}";
            }

            return "No seats in the classroom";
        }

        public string DismissStudent(string firstName, string lastName)
        {
            if (studentsInClass.Any(Student=> Student.FirstName == firstName && Student.LastName==lastName))
            {
                Student studentToDismiss = studentsInClass.FirstOrDefault(Student => Student.FirstName == firstName && Student.LastName == lastName);

                studentsInClass.Remove(studentToDismiss);

               return $"Dismissed student {studentToDismiss.FirstName} {studentToDismiss.LastName}";
            }

            return "Student not found";
        }

        public string GetSubjectInfo(string subject)
        {
            List<Student> currantSubjectStudents = studentsInClass.Where(student => student.Subject == subject).ToList();

            if (currantSubjectStudents.Count > 0)
            {
                StringBuilder subjectInfo = new StringBuilder();
                subjectInfo.AppendLine($"Subject: {subject}");
                subjectInfo.AppendLine($"Students:");

                foreach (var student in currantSubjectStudents)
                {
                    subjectInfo.AppendLine($"{student.FirstName} {student.LastName}");
                }

                return subjectInfo.ToString().Trim();
            }

            return "No students enrolled for the subject";
        }

        public int GetStudentsCount()
        {
            return this.Count;
        }

        public Student GetStudent(string firstName, string lastName)
        {
            if (studentsInClass.Any(Student => Student.FirstName == firstName && Student.LastName == lastName))
            {
                Student student = studentsInClass.FirstOrDefault(Student => Student.FirstName == firstName && Student.LastName == lastName);
                return student;
            }

            return null;
        }
    }
}
