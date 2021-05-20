namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            StringReader reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(List<ProjectImportDto>), new XmlRootAttribute("Projects"));

            var projects = (List<ProjectImportDto>)serializer.Deserialize(reader);
            var projectsToImport = new List<Project>();

            foreach (var currantProject in projects)
            {
                bool isValidOpenDate = DateTime.TryParseExact(currantProject.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDate);

                DateTime? dueDate;

                if (string.IsNullOrEmpty(currantProject.DueDate))
                {
                    dueDate = null;
                }
                else
                {
                    dueDate = DateTime.ParseExact(currantProject.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                if (IsValid(currantProject) && isValidOpenDate)
                {
                    var tasks = new List<Task>();

                    foreach (var currantTask in currantProject.Tasks)
                    {
                        bool isValidTaskOpenDate = DateTime.TryParseExact(currantTask.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskOpenDate);

                        bool isValidTaskDueDate = DateTime.TryParseExact(currantTask.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskDueDate);

                        if (IsValid(currantTask) && isValidTaskDueDate && isValidTaskOpenDate
                            && taskOpenDate>=openDate && (taskDueDate <= dueDate || dueDate == null))
                        {
                            var task = new Task
                            {
                                Name = currantTask.Name,
                                OpenDate = taskOpenDate,
                                DueDate = taskDueDate,
                                ExecutionType = (ExecutionType)currantTask.ExecutionType,
                                LabelType = (LabelType)currantTask.LableType
                            };

                            tasks.Add(task);
                        }
                        else
                        {
                            result.AppendLine(ErrorMessage);
                        }
                    }

                    var project = new Project
                    {
                        Name = currantProject.Name,
                        OpenDate = openDate,
                        DueDate = dueDate,
                        Tasks = tasks
                    };

                    projectsToImport.Add(project);
                    result.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.AddRange(projectsToImport);
            context.SaveChanges();
            return result.ToString().Trim();
        }
        
        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var employees = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);
            var employeesToImport = new List<Employee>();
            var tasksIds = context.Tasks.Select(x => x.Id).ToList();

            foreach (var currantEmployee in employees)
            {
                var tasksToImport = new List<int>();

                if (IsValid(currantEmployee))
                {
                    foreach (var task in currantEmployee.Tasks.Distinct())
                    {
                        if(tasksIds.Contains(task))
                        {
                            tasksToImport.Add(task);
                        }
                        else
                        {
                            result.AppendLine(ErrorMessage);
                        }
                    }

                    var employee = new Employee
                    {
                        Username = currantEmployee.Username,
                        Email = currantEmployee.Email,
                        Phone = currantEmployee.Phone,
                        EmployeesTasks = tasksToImport.Select(x=> new EmployeeTask { TaskId = x }).ToList()
                    };

                    employeesToImport.Add(employee);
                    result.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.AddRange(employeesToImport);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}