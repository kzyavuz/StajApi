namespace DTO.DTOs.WorkDTO
{
    public class CreateWorkDto
    {
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public decimal WorkPrice { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string WorkLocal { get; set; }
        public byte WorkEmployeeCount { get; set; }
        public int EmployeeID { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ActiveDateTime { get; set; }

    }
}
