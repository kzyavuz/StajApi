namespace DTO.DTOs.WorkDTO
{
    public class ResultWorkDto
    {
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public decimal WorkPrice { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string WorkLocal { get; set; }
        public byte WorkEmployeeCount { get; set; }
        public int EmployeeID { get; set; }
        public string Status { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime PassiveDateTime { get; set; }

    }
}
