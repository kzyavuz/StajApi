using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.EmployeeDTO
{
    public class ConvertEmployeeStatusDto
    {
        public int EmployeeID { get; set; }
        public bool Status2 { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public DateTime PassiveDateTime { get; set; }
    }
}
