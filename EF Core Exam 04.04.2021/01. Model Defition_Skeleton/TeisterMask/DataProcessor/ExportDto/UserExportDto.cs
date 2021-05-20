using System;
using System.Collections.Generic;
using System.Text;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class UserExportDto
    {
        public string Username { get; set; }

        public TaskExportDto[] Tasks { get; set; }
    }
}
