namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projectsToExport = context.Projects
                .ToList()
                .Where(x => x.Tasks.Count > 0)
                .Select(x => new ProjectExportDto
                {
                    ProjectName = x.Name,
                    TasksCount = x.Tasks.Count,
                    HasEndDate = x.DueDate.ToString(),
                    Tasks = x.Tasks.Select(t => new TaskProjectExportDto
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString(),
                    })
                    .OrderBy(x => x.Name)
                    .ToArray()
                })
                .OrderByDescending(x => x.TasksCount)
                .ThenBy(x => x.ProjectName)
                .ToList();

            foreach (var item in projectsToExport)
            {
                if(string.IsNullOrEmpty(item.HasEndDate))
                {
                    item.HasEndDate = "No";
                }
                else
                {
                    item.HasEndDate = "Yes";
                }
            }

            var serializer = new XmlSerializer(typeof(List<ProjectExportDto>), new XmlRootAttribute("Projects"));

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var writer = new StringWriter();

            serializer.Serialize(writer, projectsToExport, ns);

            return writer.ToString();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employeesToExport = context.Employees
                .ToList()
                .Where(x => x.EmployeesTasks.Any(task => task.Task.OpenDate >= date))
                .Select(x => new UserExportDto
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks
                    .Where(t=>t.Task.OpenDate >= date)
                    .OrderByDescending(t => t.Task.DueDate)
                    .ThenBy(t => t.Task.Name)
                    .Select(t => new TaskExportDto
                    {
                        TaskName = t.Task.Name,
                        OpenDate = t.Task.OpenDate.ToString("MM/dd/yyyy"),
                        DueDate = t.Task.DueDate.ToString("MM/dd/yyyy"),
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString(),
                    })
                    .ToArray()
                })
                .OrderByDescending(x=>x.Tasks.Length)
                .ThenBy(x=>x.Username)
                .Take(10)
                .ToList();

            var output = JsonConvert.SerializeObject(employeesToExport,Formatting.Indented);

            return output;
        }
    }
}