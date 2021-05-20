using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectExportDto
    {
        [XmlAttribute]
        public int TasksCount { get; set; }

        [XmlElement]
        public string ProjectName { get; set; }

        [XmlElement]
        public string HasEndDate { get; set; }

        [XmlArray]
        public TaskProjectExportDto[] Tasks { get; set; }

    }

}
