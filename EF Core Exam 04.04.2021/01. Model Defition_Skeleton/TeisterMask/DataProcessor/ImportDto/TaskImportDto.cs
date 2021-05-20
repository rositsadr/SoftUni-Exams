﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskImportDto
    {
        [XmlElement]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [XmlElement]
        [Required]
        public string OpenDate { get; set; }

        [XmlElement]
        [Required]
        public string DueDate { get; set; }

        public int ExecutionType { get; set; }

        public int LableType { get; set; }
    }
}