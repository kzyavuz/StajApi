namespace StajApi.DTOs.WorkDTO
{
    public class UpdateWorkDto
    {
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public int EmployeeID { get; set; }
        public int DealerID { get; set; }
        public bool Status { get; set; }
    }
}
