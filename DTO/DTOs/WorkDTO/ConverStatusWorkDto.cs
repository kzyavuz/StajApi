using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.WorkDTO
{
    public class ConverStatusWorkDto
    {
        public int WorkID { get; set; }
        public bool Status2 { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public DateTime PassiveDateTime { get; set; }
    }
}
