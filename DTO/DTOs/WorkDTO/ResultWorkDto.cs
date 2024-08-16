namespace DTO.DTOs.WorkDTO
{
    public class ResultWorkDto
    {
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurName { get; set; }
        public decimal WorkPrice { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string WorkLocal { get; set; }
        public int EmployeeID { get; set; }
        public bool Status { get; set; }
        public bool Status2 { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime PassiveDateTime { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public DateTime DeleteDateTime { get; set; }

    }
}
