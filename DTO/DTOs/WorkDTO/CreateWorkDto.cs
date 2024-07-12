namespace DTO.DTOs.WorkDTO
{
    public class CreateWorkDto
    {
        public string WorkName { get; set; }
        public int EmployeeID { get; set; }
        public int DealerID { get; set; }
        public bool Status { get; set; }

    }
}
