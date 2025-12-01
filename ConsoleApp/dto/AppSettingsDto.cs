using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.dto;

public class AppSettingsDto
{
    public required string AppName { get; set; }
    public int RunIntervalSeconds { get; set; }
}